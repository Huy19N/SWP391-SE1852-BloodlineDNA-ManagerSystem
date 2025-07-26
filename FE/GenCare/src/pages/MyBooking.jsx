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
    const [showFeedbackModal, setShowFeedbackModal] = useState(false);
    const [feedbackText, setFeedbackText] = useState("");
    const [currentBookingId, setCurrentBookingId] = useState(null);
    const [rating, setRating] = useState(0); 
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
        
        const dataStatus = resStatus.data.data;
        console.log(dataStatus);
        
        localStorage.setItem("statusId_Complete", dataStatus.find(s => s.statusName === "Hoàn thành")?.statusName);

        
        setDataBooking(resBooking.data.data.filter((b) => b.userId === userId));
        setDataService(resService.data.data);
        setDataDuration(resDuration.data.data);
        setDataStatus(resStatus.data.data);
        } catch (error) {
        toast.error("Thất bại tải dữ liệu đặt chỗ");
        } finally {
        setIsLoading(false);
        }
    };

    const getServiceName = (id) => dataService.find((s) => s.serviceId === id)?.serviceName || "Trống";
    const getServiceType = (id) => dataService.find((s) => s.serviceId === id)?.serviceType || "Trống";
    const getDurationName = (id) => dataDuration.find((d) => d.durationId === id)?.durationName || "Trống";
    const getStatusName = (id) => dataStatus.find((s) => s.statusId === id)?.statusName || "Trống";

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
            sampleName: sampleMap[p.sampleId] || "Không"
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
        toast.error("Thất bại tải nội dung đặt chỗ");
        }
    };
    

    const exportToPDF = () => {
    if (!detailData || !pdfRef.current) return;

    const opt = {
        margin: 0.5,
        filename: `Booking_${detailData.booking?.user?.fullName || "Không"}_${detailData.booking?.service?.serviceName || "Service"}.pdf`,
        image: { type: "jpeg", quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: "in", format: "a4", orientation: "portrait" },
    };

    html2pdf().set(opt).from(pdfRef.current).save();
    };

    useEffect(() => {
        fetchData();
    }, []);

    const handleOpenFeedback = (bookingId) => {
    setCurrentBookingId(bookingId);
    setShowFeedbackModal(true);
    };

    const handleSubmitFeedback = async () => {
    if (!feedbackText) {
        toast.warning("Làm ơn nhập phản hồi của bạn.");
        return;
    }

    try {
        await api.post("Feedbacks/Create", {
        userId: userId,
        serviceId: dataBooking.find(b => b.bookingId === currentBookingId)?.serviceId || 0,
        createdAt: new Date().toISOString(),
        comment: feedbackText,
        rating: rating,
    });
        toast.success("Cảm ơn bạn đã đưa ra lời khuyên cho chúng tôi!");
        setShowFeedbackModal(false);
        setFeedbackText("");
    } catch (error) {
        toast.error("Thất bại tạo phản hồi.");
    }
};

    const filterBookings = dataBooking.filter((booking) => {
        const keyword = search.toLowerCase();
        return (
        booking.bookingId.toString().includes(keyword) ||
        getServiceName(booking.serviceId).toLowerCase().includes(keyword) ||
        getStatusName(booking.statusId).toLowerCase().includes(keyword) ||
        booking.appointmentTime?.split("T")[0].toString().includes(keyword) ||
        getServiceType(booking.serviceId).toLowerCase().includes(keyword) || 
        getDurationName(booking.durationId).toLowerCase().includes(keyword)
        );
    });

    const statusId_Complete = localStorage.getItem("statusId_Complete");

    return (
        <div className="container mt-5 mb-5 min-vw-100 min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
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
                    <Link className="text-light text-decoration-none" to="/services">Thêm</Link>
                </button>
                </div>
            </div>

            <table className="table table-bordered table-hover align-middle shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>Mã</th>
                    <th>Tên Dịch Vụ</th>
                    <th>Loại Dịch Vụ</th>
                    <th>Thời Lượng</th>
                    <th>Ngày Đặt chỗ</th>
                    <th>Trạng Thái</th>
                    <th>Hành Động</th>
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr><td colSpan="7">Tải...</td></tr>
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
                        <button className="btn btn-sm btn-info me-3 ms-3" onClick={() => fetchBookingDetail(booking.bookingId)}>Xem chi tiết</button>
                        {getStatusName(booking.statusId) === statusId_Complete ? 
                        <button className="btn btn-sm btn-success" onClick={() => handleOpenFeedback(booking.bookingId)}>Phản hồi</button>
                        : null}
                        </td>
                    </tr>
                    ))
                ) : (
                    <tr><td colSpan="7" className="text-center">Hiện bạn chưa có đặt nào.</td></tr>
                )}
                </tbody>
            </table>

            {/* Overlay */}
            {showOverlay && detailData && (
                <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center">
                    <div className="bg-white p-4 rounded shadow border-bottom" style={{ width: "80%", maxHeight: "90vh", overflowY: "auto" }}>
                        <div>
                            <h5 className="mb-3 border-bottom border-primary pb-2 text-center">Nội dung của Đặt Chỗ của bạn - #{detailData.booking.bookingId}</h5>
                        </div>
                        {console.log(detailData.booking.status?.statusId)}
                        {/* Process Test realtime */}
                        {(detailData.booking.status?.statusName === "Đang thực hiện" || detailData.booking.status?.statusName === "Hoàn thành") && (
                        <div className="mb-3 border-bottom border-primary pb-2">
                        <h5 className="m-4 text-center">Quá trình kiểm tra</h5>
                            <TimeLine
                            steps={resStepsAll}
                            processes={detailData.processes}
                            />
                        </div>
                        )}
                        

                        {/* Service Info */}
                        <div className="mb-3 border-bottom border-primary pb-2">
                            <h4 className="text-success">Nội Dung Dịch Vụ</h4>
                            <p><strong>Tên Dịch Vụ:</strong> {detailData.booking.service?.serviceName}</p>
                            <p><strong>Loại Dịch Vụ:</strong> {detailData.booking.service?.serviceType}</p>
                            <p><strong>Thời Lượng:</strong> {detailData.booking.duration?.durationName}</p>
                            <p><strong>Phương thức thu mẫu:</strong> {detailData.booking.collectionMethod?.methodName}</p>
                            <p><strong>Ngày Đăng Ký:</strong> {detailData.booking?.date?.split("T")[0]}</p>
                            <p><strong>Ngày Đặt Chỗ:</strong> {detailData.booking?.appointmentTime?.split("T")[0]}</p>
                            <p><strong>Trạng Thái:</strong> {detailData.booking.status?.statusName}</p>
                            {detailData.booking.status?.statusName === "Hoàn thành" ?
                            <p><strong>Kết Quả:</strong> {detailData.booking.result?.resultSummary}</p>
                            : <p><strong>Kết Quả:</strong> Chưa Có Kết Quả.</p>}
                            {detailData.booking.result?.resultSummary && detailData.booking.result?.date && detailData.booking.status?.statusName === "Hoàn thành" &&(
                                <p><strong>Ngày Công Bố Kết Quả:</strong> {detailData.booking.result?.date.split("T")[0]}</p>
                            )}
                        </div>

                        <div className="mb-3 border-bottom border-primary pb-2">
                            <h5>Thông TIn Người Dùng</h5>
                            <p><strong>Tên:</strong> {detailData.booking.user?.fullName}</p>
                            <p><strong>Email:</strong> {detailData.booking.user?.email}</p>
                            <p><strong>Số Điên Thoại:</strong> {detailData.booking.user?.phone}</p>
                            <p><strong>Vị Trí:</strong> {detailData.booking.user?.address}</p>
                            <p><strong>CCCD:</strong> {detailData.booking.user?.identifyId}</p>
                        </div>

                        <div>
                            <h5>Thông Tin Người Xét Nghiệm</h5>
                            {detailData.patients.length > 0 ? (
                                <table className="table table-bordered">
                                <thead>
                                    <tr>
                                    <th>Tên Đầy ĐỦ</th>
                                    <th>Ngày Sinh</th>
                                    <th>Giới Tính</th>
                                    <th>CCCD</th>
                                    <th>Mẫu</th>
                                    <th>Mỗi Quan Hệ Dự Đoán</th>
                                    <th>Đã Xét Nghiệm Chưa</th>
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
                                        {p.hasTestedDna ? "Rồi" : "Chưa"}
                                        </td>
                                    </tr>
                                    ))}
                                </tbody>
                                </table>
                            ) : <p>Không Có dữ liệu.</p>}
                        </div>
                        
                        <button className="btn btn-success me-2" onClick={exportToPDF}>Tải Tệp PDF</button>
                        <button className="btn btn-danger float-end" onClick={() => setShowOverlay(false)}>Đóng</button>
                    </div>
                </div>
            )}

            {/* PDF */}

            {detailData && (
            <div style={{ display: "none" }}>
                <div ref={pdfRef} className="p-4" style={{ fontFamily: 'Arial, sans-serif', width: "800px" }}>
                        <h2 style={{ borderBottom: "2px solid #007bff", paddingBottom: "10px" }}>Nội Dung Đặt Chỗ - #{detailData.booking.bookingId}</h2>

                        {/* Service Info */}
                        <h4 style={{ color: "#007bff" }}>Thông Tin Dịch Vụ</h4>
                        <table style={{ width: "100%", marginBottom: "20px" }}>
                        <tbody>
                            <tr><td><strong>Tên Dịch Vụ:</strong></td><td>{detailData.booking.service?.serviceName}</td></tr>
                            <tr><td><strong>Loại Dịch Vụ:</strong></td><td>{detailData.booking.service?.serviceType}</td></tr>
                            <tr><td><strong>Thời Lượng:</strong></td><td>{detailData.booking.duration?.durationName}</td></tr>
                            <tr><td><strong>Phương Thức Thu Mẫu:</strong></td><td>{detailData.booking.collectionMethod?.methodName}</td></tr>
                            <tr><td><strong>Ngày Đăng Ký:</strong></td><td>{detailData.booking.date?.split("T")[0]}</td></tr>
                            <tr><td><strong>Ngày Đặt Chỗ:</strong></td><td>{detailData.booking.appointmentTime?.split("T")[0]}</td></tr>
                            <tr><td><strong>Trạng Thái:</strong></td><td>{detailData.booking.status?.statusName}</td></tr>
                            <tr><td><strong>Kết Quả:</strong></td><td>{detailData.booking.result?.resultSummary || "Not yet"}</td></tr>
                            {detailData.booking.result?.date && (
                            <tr><td><strong>Ngày Công Bố Kết Quả:</strong></td><td>{detailData.booking.result.date?.split("T")[0]}</td></tr>
                            )}
                        </tbody>
                        </table>

                        {/* User Info */}
                        <h4 style={{ color: "#007bff" }}>Thông Tin Người Dùng</h4>
                        <table style={{ width: "100%", marginBottom: "20px" }}>
                        <tbody>
                            <tr><td><strong>Tên:</strong></td><td>{detailData.booking.user?.fullName}</td></tr>
                            <tr><td><strong>Email:</strong></td><td>{detailData.booking.user?.email}</td></tr>
                            <tr><td><strong>Số Điện thoại:</strong></td><td>{detailData.booking.user?.phone}</td></tr>
                            <tr><td><strong>Vị Trí:</strong></td><td>{detailData.booking.user?.address}</td></tr>
                            <tr><td><strong>CCCD:</strong></td><td>{detailData.booking.user?.identifyId}</td></tr>
                        </tbody>
                        </table>

                        {/* Patient Info */}
                        <h4 style={{ color: "#007bff" }}>Thông Tin Khách Hàng Xét Nghiệm</h4>
                        {detailData.patients.length > 0 ? (
                        <table style={{ width: "100%", borderCollapse: "collapse" }} border="1">
                            <thead>
                            <tr>
                                <th>Tên Đầy ĐỦ</th>
                                <th>Ngày Sinh</th>
                                <th>Giới Tính</th>
                                <th>CCCD</th>
                                <th>Mẫu</th>
                                <th>Mối Quan Hệ Dự Đoán</th>
                                <th>Đã Xét Nghiệm Chưa</th>
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
                                <td>{p.hasTestedDna ? "Rồi" : "Chưa"}</td>
                                </tr>
                            ))}
                            </tbody>
                        </table>
                        ) : <p>Không có dữ liệu</p>}

                        {detailData.booking.result?.resultSummary ? 
                        <div style={{textAlign: "start", marginTop: "10px" }}>
                            <p><strong>Người lập phiếu:</strong> CN. Nguyễn Gia Huy</p>
                            <p><strong>Ngày:</strong> {new Date().toLocaleDateString()}</p>
                        </div>
                        : null }

                        {detailData.booking.result?.resultSummary ? 
                        <div style={{marginLeft: "500px", marginTop: "40px" }}>
                            <p>Giám Đốc</p>
                        </div>
                        : null }

                        {detailData.booking.result?.resultSummary ? 
                        <div style={{marginLeft: "480px", marginTop: "10px" }}>
                            <img src={img1} alt="stamp" style={{ width: "120px", opacity: 0.6 }} />
                        </div>
                        : null }

                        {detailData.booking.result?.resultSummary ? 
                        <div style={{marginLeft: "460px", marginTop: "10px" }}>
                            <p> CN. Nguyễn Gia Huy</p>
                        </div>
                        : null }

                </div>
            </div>
            )}

            {/* Feedback */}
            {showFeedbackModal && (
                <div className="modal fade show d-block" tabIndex="-1" style={{ backgroundColor: "rgba(0, 0, 0, 0.5)" }}>
                    <div className="modal-dialog">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h3 className="modal-title text-center">Phản Hồi</h3>
                                <button type="button" className="btn-close" onClick={() => setShowFeedbackModal(false)}></button>
                            </div>
                            <div>
                                <p className="text-center">Cảm Ơn bạn đã tham gia dịch vụ của chúng tôi có phiền không nếu bạn có thể để lại cho chúng tôi một ít cảm nghĩ của bạn về dịch vụ</p>
                            </div>
                            <div className="modal-body">
                                <div className="d-flex justify-content-center mb-3">
                                    {[1, 2, 3, 4, 5].map((star) => (
                                        <span
                                        key={star}
                                        style={{ fontSize: "24px", cursor: "pointer", color: star <= rating ? "gold" : "gray" }}
                                        onClick={() => setRating(star)}
                                        >
                                        ★
                                        </span>
                                    ))}
                                </div>
                                <textarea
                                    className="form-control"
                                    rows="4"
                                    value={feedbackText}
                                    onChange={(e) => setFeedbackText(e.target.value)}
                                    placeholder="Viết Cảm nghĩ của bạn tại đây..."
                                />
                            </div>
                            <div className="modal-footer">
                                <button className="btn btn-secondary" onClick={() => setShowFeedbackModal(false)}>Đóng</button>
                                <button className="btn btn-primary" onClick={handleSubmitFeedback}>Nộp</button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div> 
    );
    }

export default MyBooking;
