import React, { useEffect, useState } from "react";
import api from '../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';
import { href, Link } from "react-router-dom";

function MyBooking() {
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataDuration, setDataDuration] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);
    const [search, setSearch] = useState('');
    const [detailData, setDetailData] = useState(null);
    const [showOverlay, setShowOverlay] = useState(false);

    const userId = parseInt(localStorage.getItem('userId'));

    const fetchData = async () => {
        setIsLoading(true);
        try {
            const [resBooking, resService, resDuration, resStatus] = await Promise.all([
                api.get('Bookings/GetAll'),
                api.get('Services/GetAllPaging'),
                api.get('Durations/GetAllPaging'),
                api.get('Status/GetAllStatus'),
            ]);

            setDataBooking(resBooking.data.data.filter(b => b.userId === userId));
            setDataService(resService.data.data);
            setDataDuration(resDuration.data.data);
            setDataStatus(resStatus.data.data);
        } catch (error) {
            toast.error("Failed to load booking data");
        } finally {
            setIsLoading(false);
        }
    };

    const getServiceName = (id) => dataService.find(s => s.serviceId === id)?.serviceName || 'Empty';
    const getServiceType = (id) => dataService.find(s => s.serviceId === id)?.serviceType || 'Empty';
    const getDurationName = (id) => dataDuration.find(d => d.durationId === id)?.durationName || 'Empty';
    const getStatusName = (id) => dataStatus.find(s => s.statusId === id)?.statusName || 'Empty';

    const fetchBookingDetail = async (bookingId) => {
        try {
            const [
                resBooking, resUserAll, resPatientAll, resSampleAll,
                resProcessAll, resServiceAll, resStatusAll, resDurationAll,
                resMethodAll, resStepsAll, resResults
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

    useEffect(() => {
        fetchData();
    }, []);


    const filterBookings = dataBooking.filter((bookings) => {
        const keyword = search.toLowerCase();
        return(
                bookings.bookingId.toString().includes(keyword) ||
                getServiceName(bookings.serviceId).toLowerCase().includes(keyword) ||
                getStatusName(bookings.statusId).toLowerCase().includes(keyword) ||
                bookings.appointmentTime?.split("T")[0].toString().includes(keyword) ||
                getServiceType(bookings.serviceId).toLowerCase().includes(keyword)
            )
    });

    return (
        <div className="container mt-5 mb-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary">
                    My Booking
                </div>

            <div className="row mb-3">
                    <div className="col-md-4">
                        <input
                            type="text"
                            placeholder="Search......."
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
                        <tr>
                                <td colSpan="7" className="text-center">No booking data found.</td>
                        </tr>
                    )}
                </tbody>
            </table>

            {/* Overlay Detail View (reuse your existing overlay component for detailData) */}
            {showOverlay && detailData && (
                <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center">
                    <div className="bg-white p-4 rounded shadow" style={{ width: "80%", maxHeight: "90vh", overflowY: "auto" }}>
                        <h5>Booking Detail - #{detailData.booking.bookingId}</h5>
                        <p><strong>Service Name:</strong> {detailData.booking.service?.serviceName}</p>
                        <p><strong>Service Type:</strong> {detailData.booking.service?.serviceType}</p>
                        <p><strong>Duration:</strong> {detailData.booking.duration?.durationName}</p>
                        <p><strong>Collection Method:</strong> {detailData.booking.collectionMethod?.methodName}</p>
                        <p><strong>Date:</strong> {detailData.booking?.date.split("T")[0]}</p>
                        <p><strong>Appointment:</strong> {detailData.booking?.appointmentTime.split("T")[0]}</p>
                        <p><strong>Status:</strong> {detailData.booking.status?.statusName}</p>
                        <button className="btn btn-danger float-end" onClick={() => setShowOverlay(false)}>Close</button>
                    </div>

                    {/* 2. User Info */}
                    {/* <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center">
                        <h2 className="">
                                2. Information User
                        </h2>
                        <div>
                        <div className="bg-white p-4 rounded shadow" style={{ width: "80%", maxHeight: "90vh", overflowY: "auto" }}>
                            <p><strong>Full Name:</strong> {detailData.booking.user?.fullName}</p>
                            <p><strong>Email:</strong> {detailData.booking.user?.email}</p>
                            <p><strong>Phone:</strong> {detailData.booking.user?.phone}</p>
                            <p><strong>Identify ID:</strong> {detailData.booking.user?.identifyId}</p>
                            <p><strong>Address:</strong> {detailData.booking.user?.address}</p>
                        </div>
                        </div>
                    </div> */}
                </div>
            )}

            <ToastContainer />
        </div>
    );
}

export default MyBooking;
