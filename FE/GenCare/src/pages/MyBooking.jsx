import React, { useEffect, useState, useRef } from "react";
import html2pdf from "html2pdf.js";
import api from "../config/axios.js";
import img1 from "../assets/GenCare.png";
import TimeLine from "../components/TimeLine.jsx"
import { ToastContainer, toast } from "react-toastify";
import { Link } from "react-router-dom";

    function MyBooking() {
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataDuration, setDataDuration] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);
    const [resStepsAll, setResStepsAll] = useState([]);
    const [search, setSearch] = useState("");
    const [detailData, setDetailData] = useState(null);
    const [showOverlay, setShowOverlay] = useState(false);
    const pdfRef = useRef();

    const userId = parseInt(localStorage.getItem("userId"));

    const fetchData = async () => {
        setIsLoading(true);
        try {
        const [resBooking, resService, resDuration, resStatus] = await Promise.all([
            api.get("Bookings/GetAll"),
            api.get("Services/GetAllPaging"),
            api.get("Durations/GetAllPaging"),
            api.get("Status/GetAllStatus"),
        ]);
        
        setDataBooking(resBooking.data.data.filter((b) => b.userId === userId));
        setDataService(resService.data.data);
        setDataDuration(resDuration.data.data);
        setDataStatus(resStatus.data.data);
        } catch (error) {
        toast.error("Failed to load booking data");
        } finally {
        setIsLoading(false);
        }
    };

    const getServiceName = (id) => dataService.find((s) => s.serviceId === id)?.serviceName || "Empty";
    const getServiceType = (id) => dataService.find((s) => s.serviceId === id)?.serviceType || "Empty";
    const getDurationName = (id) => dataDuration.find((d) => d.durationId === id)?.durationName || "Empty";
    const getStatusName = (id) => dataStatus.find((s) => s.statusId === id)?.statusName || "Empty";

    const fetchBookingDetail = async (bookingId) => {
        try {
        const [
            resBooking, 
            resUserAll, 
            resPatientAll, 
            resSampleAll,
            resProcessAll, 
            resServiceAll, 
            resStatusAll, 
            resDurationAll,
            resMethodAll, 
            resStepsAll, 
            resResults
        ] = await Promise.all([
            api.get(`Bookings/GetById/${bookingId}`),
            api.get(`Users/GetAll`),
            api.get(`Patient/GetAll`),
            api.get(`Samples/GetAllPaging`),
            api.get(`TestProcess/GetAllPaging`),
            api.get(`Services/GetAllPaging`),
            api.get(`Status/GetAllStatus`),
            api.get(`Durations/GetAllPaging`),
            api.get(`CollectionMethod/GetAll`),
            api.get(`TestStep/getAllTestSteps`),
            api.get(`TestResults/GetAllPaging`)
        ]);

        const booking = resBooking.data.data;
        const step = resStepsAll.data.data;
        const user = resUserAll.data.data.find(u => u.userId === booking.userId);
        const service = resServiceAll.data.data.find(s => s.serviceId === booking.serviceId);
        const status = resStatusAll.data.data.find(s => s.statusId === booking.statusId);
        const method = resMethodAll.data.data.find(m => m.methodId === booking.methodId);
        const duration = resDurationAll.data.data.find(d => d.durationId === booking.durationId);
        const result = resResults.data.find(r => r.resultId === booking.resultId);

        const sampleMap = {};
        resSampleAll.data.data.forEach(sample => {
            sampleMap[sample.sampleId] = sample.sampleName;
        });

        const patients = resPatientAll.data.data
            .filter(p => p.bookingId === bookingId)
            .map(p => ({
            ...p,
            sampleName: sampleMap[p.sampleId] || "Unknown"
            }));

        const steps = resStepsAll.data.data;
        const processes = resProcessAll.data.data
            .filter(p => p.bookingId === bookingId)
            .map(p => ({
            ...p,
            step: steps.find(s => s.stepId === p.stepId),
            status: resStatusAll.data.data.find(st => st.statusId === p.statusId)
            }));
        
        // Set detail data
        setResStepsAll(step);
        setDetailData({
            booking: { ...booking, user, service, status, duration, collectionMethod: method, result },
            patients,
            samples: resSampleAll.data.data,
            processes
        });

        setShowOverlay(true);
        } catch (error) {
        toast.error("Failed to load booking detail");
        }
    };
    

    const exportToPDF = () => {
    if (!detailData || !pdfRef.current) return;

    const opt = {
        margin: 0.5,
        filename: `Booking_${detailData.booking?.user?.fullName || "Unknown"}_${detailData.booking?.service?.serviceName || "Service"}.pdf`,
        image: { type: "jpeg", quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: "in", format: "a4", orientation: "portrait" },
    };

    html2pdf().set(opt).from(pdfRef.current).save();
    };
    useEffect(() => {
        fetchData();
    }, []);

    const filterBookings = dataBooking.filter((booking) => {
        const keyword = search.toLowerCase();
        return (
        booking.bookingId.toString().includes(keyword) ||
        getServiceName(booking.serviceId).toLowerCase().includes(keyword) ||
        getStatusName(booking.statusId).toLowerCase().includes(keyword) ||
        booking.appointmentTime?.split("T")[0].toString().includes(keyword) ||
        getServiceType(booking.serviceId).toLowerCase().includes(keyword)
        );
    });

    return (
        <div className="container mt-5 mb-5">
        <h2 className="text-primary border-bottom border-primary pb-2 mb-4">My Booking</h2>

        <div className="row mb-3">
            <div className="col-md-4">
            <input
                type="text"
                placeholder="Search..."
                className="form-control"
                value={search}
                onChange={(e) => setSearch(e.target.value)}
            />
            </div>
            <div className="col-md-4">
            <button className="btn btn-primary">
                <Link className="text-light text-decoration-none" to="/services">New Booking</Link>
            </button>
            </div>
        </div>

        <table className="table table-bordered table-hover align-middle shadow">
            <thead className="table-primary text-center">
            <tr>
                <th>ID</th>
                <th>Service Name</th>
                <th>Service Type</th>
                <th>Duration</th>
                <th>Appointment</th>
                <th>Status</th>
                <th>Action</th>
            </tr>
            </thead>
            <tbody>
            {isLoading ? (
                <tr><td colSpan="7">Loading...</td></tr>
            ) : filterBookings.length > 0 ? (
                filterBookings.map((booking) => (
                <tr key={booking.bookingId}>
                    <td>#{booking.bookingId}</td>
                    <td>{getServiceName(booking.serviceId)}</td>
                    <td>{getServiceType(booking.serviceId)}</td>
                    <td>{getDurationName(booking.durationId)}</td>
                    <td>{booking.appointmentTime?.split("T")[0]}</td>
                    <td>{getStatusName(booking.statusId)}</td>
                    <td>
                    <button className="btn btn-sm btn-info" onClick={() => fetchBookingDetail(booking.bookingId)}>View</button>
                    </td>
                </tr>
                ))
            ) : (
                <tr><td colSpan="7" className="text-center">No booking data found.</td></tr>
            )}
            </tbody>
        </table>

        {/* Overlay */}
        {showOverlay && detailData && (
            <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center">
                <div className="bg-white p-4 rounded shadow border-bottom" style={{ width: "80%", maxHeight: "90vh", overflowY: "auto" }}>
                    <div>
                        <h5 className="mb-3 border-bottom border-primary pb-2 text-center">Booking Detail - #{detailData.booking.bookingId}</h5>
                    </div>
                    {console.log(detailData.booking.status?.statusId)}
                    {/* Process Test realtime */}
                    {(detailData.booking.status?.statusName === "Đang thực hiện" || detailData.booking.status?.statusName === "Hoàn thành") && (
                    <div className="mb-3 border-bottom border-primary pb-2">
                    <h5 className="m-4 text-center">Process Test</h5>
                        <TimeLine
                        steps={resStepsAll}
                        processes={detailData.processes}
                        />
                    </div>
                    )}
                    

                    {/* Service Info */}
                    <div className="mb-3 border-bottom border-primary pb-2">
                        <h5>Service Info</h5>
                        <p><strong>Service Name:</strong> {detailData.booking.service?.serviceName}</p>
                        <p><strong>Service Type:</strong> {detailData.booking.service?.serviceType}</p>
                        <p><strong>Duration:</strong> {detailData.booking.duration?.durationName}</p>
                        <p><strong>Collection Method:</strong> {detailData.booking.collectionMethod?.methodName}</p>
                        <p><strong>Date:</strong> {detailData.booking?.date?.split("T")[0]}</p>
                        <p><strong>Appointment:</strong> {detailData.booking?.appointmentTime?.split("T")[0]}</p>
                        <p><strong>Status:</strong> {detailData.booking.status?.statusName}</p>
                        <p><strong>Result:</strong> {detailData.booking.result?.resultSummary || "Not result yet"}</p>
                        {detailData.booking.result?.resultSummary && detailData.booking.result?.date && (
                            <p><strong>Date Public Result:</strong> {detailData.booking.result?.date.split("T")[0]}</p>
                        )}
                    </div>

                    <div className="mb-3 border-bottom border-primary pb-2">
                        <h5>User Info</h5>
                        <p><strong>Name:</strong> {detailData.booking.user?.fullName}</p>
                        <p><strong>Email:</strong> {detailData.booking.user?.email}</p>
                        <p><strong>Phone:</strong> {detailData.booking.user?.phone}</p>
                        <p><strong>Address:</strong> {detailData.booking.user?.address}</p>
                        <p><strong>CCCD:</strong> {detailData.booking.user?.identifyId}</p>
                    </div>

                    <div>
                        <h5>Patient Info</h5>
                        {detailData.patients.length > 0 ? (
                            <table className="table table-bordered">
                            <thead>
                                <tr>
                                <th>Full Name</th>
                                <th>BirthDate</th>
                                <th>Gender</th>
                                <th>Identify ID</th>
                                <th>Sample Name</th>
                                <th>Relationship</th>
                                <th>DNA Tested</th>
                                </tr>
                            </thead>
                            <tbody>
                                {detailData.patients.map(p => (
                                <tr key={p.patientId}>
                                    <td>{p.fullName}</td>
                                    <td>{p.birthDate}</td>
                                    <td>{p.gender}</td>
                                    <td>{p.identifyId}</td>
                                    <td>{p.sampleName}</td>
                                    <td>{p.relationship}</td>
                                    <td className={p.hasTestedDna ? "text-success" : "text-danger"}>
                                    {p.hasTestedDna ? "Yes" : "No"}
                                    </td>
                                </tr>
                                ))}
                            </tbody>
                            </table>
                        ) : <p>No patient data</p>}
                    </div>

                    <button className="btn btn-success me-2" onClick={exportToPDF}>Download PDF</button>
                    <button className="btn btn-danger float-end" onClick={() => setShowOverlay(false)}>Close</button>
                </div>
            </div>
        )}

        {/* PDF */}

        {detailData && (
        <div style={{ display: "none" }}>
            <div ref={pdfRef} className="p-4" style={{ fontFamily: 'Arial, sans-serif', width: "800px" }}>
                    <h2 style={{ borderBottom: "2px solid #007bff", paddingBottom: "10px" }}>Booking Detail - #{detailData.booking.bookingId}</h2>

                    {/* Service Info */}
                    <h4 style={{ color: "#007bff" }}>Service Information</h4>
                    <table style={{ width: "100%", marginBottom: "20px" }}>
                    <tbody>
                        <tr><td><strong>Service Name:</strong></td><td>{detailData.booking.service?.serviceName}</td></tr>
                        <tr><td><strong>Service Type:</strong></td><td>{detailData.booking.service?.serviceType}</td></tr>
                        <tr><td><strong>Duration:</strong></td><td>{detailData.booking.duration?.durationName}</td></tr>
                        <tr><td><strong>Collection Method:</strong></td><td>{detailData.booking.collectionMethod?.methodName}</td></tr>
                        <tr><td><strong>Date:</strong></td><td>{detailData.booking.date?.split("T")[0]}</td></tr>
                        <tr><td><strong>Appointment:</strong></td><td>{detailData.booking.appointmentTime?.split("T")[0]}</td></tr>
                        <tr><td><strong>Status:</strong></td><td>{detailData.booking.status?.statusName}</td></tr>
                        <tr><td><strong>Result:</strong></td><td>{detailData.booking.result?.resultSummary || "Not yet"}</td></tr>
                        {detailData.booking.result?.date && (
                        <tr><td><strong>Date Public Result:</strong></td><td>{detailData.booking.result.date?.split("T")[0]}</td></tr>
                        )}
                    </tbody>
                    </table>

                    {/* User Info */}
                    <h4 style={{ color: "#007bff" }}>User Information</h4>
                    <table style={{ width: "100%", marginBottom: "20px" }}>
                    <tbody>
                        <tr><td><strong>Name:</strong></td><td>{detailData.booking.user?.fullName}</td></tr>
                        <tr><td><strong>Email:</strong></td><td>{detailData.booking.user?.email}</td></tr>
                        <tr><td><strong>Phone:</strong></td><td>{detailData.booking.user?.phone}</td></tr>
                        <tr><td><strong>Address:</strong></td><td>{detailData.booking.user?.address}</td></tr>
                        <tr><td><strong>CCCD:</strong></td><td>{detailData.booking.user?.identifyId}</td></tr>
                    </tbody>
                    </table>

                    {/* Patient Info */}
                    <h4 style={{ color: "#007bff" }}>Patient Information</h4>
                    {detailData.patients.length > 0 ? (
                    <table style={{ width: "100%", borderCollapse: "collapse" }} border="1">
                        <thead>
                        <tr>
                            <th>Full Name</th>
                            <th>BirthDate</th>
                            <th>Gender</th>
                            <th>Identify ID</th>
                            <th>Sample Name</th>
                            <th>Relationship</th>
                            <th>DNA Tested</th>
                        </tr>
                        </thead>
                        <tbody>
                        {detailData.patients.map(p => (
                            <tr key={p.patientId}>
                            <td>{p.fullName}</td>
                            <td>{p.birthDate}</td>
                            <td>{p.gender}</td>
                            <td>{p.identifyId}</td>
                            <td>{p.sampleName}</td>
                            <td>{p.relationship}</td>
                            <td>{p.hasTestedDna ? "Yes" : "No"}</td>
                            </tr>
                        ))}
                        </tbody>
                    </table>
                    ) : <p>No patient data</p>}

                    {detailData.booking.result?.resultSummary ? 
                    <div style={{ marginTop: "40px" }}>
                        <p><strong>Người lập phiếu:</strong> CN. Nguyễn Gia Huy</p>
                        <p><strong>Ngày:</strong> {new Date().toLocaleDateString()}</p>
                    </div>
                    : null }

                    {detailData.booking.result?.resultSummary ? 
                    <div style={{ textAlign: "start", marginTop: "40px" }}>
                        <img src={img1} alt="stamp" style={{ width: "120px", opacity: 0.6 }} />
                    </div>
                    : null }

            </div>
        </div>
        )}
        </div> 
    );
    }

export default MyBooking;
