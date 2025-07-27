import React, { useEffect, useState } from "react";
import api from '../../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';

function Booking(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataUser, setDataUser] = useState([]);
    const [dataServicesPrice, setDataServicesPrice] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [showOverlay, setShowOverlay] = useState(false);
    const [detailData, setDetailData] = useState(null);
    const [newResultSummary, setNewResultSummary] = useState('');
    const [newResultDate, setNewResultDate] = useState(''); 
    const [fromDataResults, setFromDataResults] = useState({
        date: '',
        resultSummary: ''
    });
    const [search, setSearch] = useState('');
    const [viewMode, setViewMode] = useState(false);

    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const [resBooking, resUser, resServicePrice, resStatus, resService] = await Promise.all([
                api.get('Bookings/GetAll'),
                api.get('Users/GetAll'),
                api.get('ServicePrices/GetAllServicePrices'),
                api.get('Status/GetAllStatus'),
                api.get('Services/GetAllPaging'),
            ]);
            const dataStatus = resStatus.data.data;
            const statusId_NotPay = dataStatus.find(s => s.statusId === 1)?.statusId;
            localStorage.setItem('statusId_Approve', statusId_NotPay);

            setDataBooking(resBooking.data.data);
            setDataUser(resUser.data.data);
            setDataServicesPrice(resServicePrice.data.data);
            setDataStatus(resStatus.data.data);
            setDataService(resService.data.data);
        }
        catch(error){
            toast.error("Error Data!");
        }
        finally{
            setIsLoading(false);
        }
    }

    const fetchDelete = async (bookingId) => {
        if(!window.confirm("Are you sure you want to delete this Booking?")) return;

        try{
            await api.delete(`Bookings/DeleteById/${bookingId}`);
            toast.success("This Booking deleted successfully!");
            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Booking this!")
        }
    }

    const fetchBookingDetail = async (bookingId) => {
        try {
            const [
                resBooking, 
                resUserAll,
                resPatientAll, 
                resSampleAll, 
                resProcessAll,
                resStatusAll,
                resMethodAll,
                resStepsAll,
                resResults,
                resServicePrice,
                resServiceAll,
                resDurationAll
            ] = await Promise.all([
                api.get(`Bookings/GetById/${bookingId}`),
                api.get(`Users/GetAll`),
                api.get(`Patient/GetAll`),
                api.get(`Samples/GetAllPaging`),
                api.get(`TestProcess/GetAllPaging`),
                api.get(`Status/GetAllStatus`),
                api.get(`CollectionMethod/GetAll`),
                api.get(`TestStep/getAllTestSteps`),
                api.get(`TestResults/GetAllPaging`),
                api.get(`ServicePrices/GetAllServicePrices`),
                api.get(`Services/GetAllPaging`),
                api.get(`Durations/GetAllPaging`)
            ]);

            const bookingList = resBooking.data.data;

            const user = resUserAll.data.data.find(u => u.userId === bookingList.userId);
            const price = resServicePrice.data.data.find(p => p.priceId === bookingList.priceId);
            const service = resServiceAll.data.data.find(s => s.serviceId === price?.serviceId);
            const duration = resDurationAll.data.data.find(d => d.durationId === price?.durationId);
            const status = resStatusAll.data.data.find(s => s.statusId === bookingList.statusId);
            const method = resMethodAll.data.data.find(m => m.methodId === bookingList.methodId);
            const result = resResults.data.find(r => r.resultId === bookingList.resultId);

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

            setDetailData({
                booking: {
                    ...bookingList,
                    user,
                    service,
                    status,
                    duration,
                    collectionMethod: method,
                    result
                },
                patients,
                samples: resSampleAll.data.data,
                processes,
            });

            setShowOverlay(true);
            setViewMode(!!result);
        } catch (error) {
            toast.error("Thất bại tải dữ liệu Của đặt chỗ");
            console.error("Lỗi của booking detail:", error);
        }
    };


    useEffect(() => {
        fetchData();
    }, []);

    const getUsername = (userId) => {
        const user = dataUser.find(u => u.userId === userId);
        return user ? user.fullName : 'Trống';
    };

    const getServiceName = (priceID) => {
        const price = dataServicesPrice.find(u => u.priceId === priceID);
        const service = dataService.find(s => s.serviceId === price?.serviceId);
        return service?.serviceName || 'Trống';
    };


    const getServiceType = (priceID) => {
        const price = dataServicesPrice.find(u => u.priceId === priceID);
        const service = dataService.find(s => s.serviceId === price?.serviceId);
        return service?.serviceType || 'Trống';
    };

    // const getDurationName = (priceID) => {
    //     const price = dataServicesPrice.find(p => p.priceId === priceID);
    //     const duration = dataDuration.find(d => d.durationId === price?.durationId);
    //     return duration?.durationName || 'Trống';
    // };

    const getStatusName = (statusId) => {
        const status = dataStatus.find(u => u.statusId === statusId);
        return status ? status.statusName : 'Trống';
    };

    const statusID_NotPay = parseInt(localStorage.getItem('statusId_NotPay') || 1);
    const filterBookings = dataBooking.filter((bookings) => {
        const keyword = search.toLowerCase();
        return(
                bookings.statusId !== statusID_NotPay && (
                bookings.bookingId.toString().includes(keyword) ||
                getUsername(bookings.userId).toLowerCase().includes(keyword) ||
                getServiceName(bookings.serviceId).toLowerCase().includes(keyword) ||
                getStatusName(bookings.statusId).toLowerCase().includes(keyword) ||
                bookings.date?.split("T")[0].toString().includes(keyword) ||
                getServiceType(bookings.serviceId).toLowerCase().includes(keyword)
            )
        );
    });

    // Cập nhật fromDataResults khi newResultSummary hoặc newResultDate thay đổi
    useEffect(() => {
        setFromDataResults({
            date: newResultDate,
            resultSummary: newResultSummary
        });
    }, [newResultSummary, newResultDate]);

    const handleCreateResult = async () => {
        if (!newResultSummary || !newResultDate) {
            toast.error("Vui lòng nhập đầy đủ kết quả tóm tắt và ngày công bố.");
            return;
        }

        try {
            setIsLoading(true);

            //  Gửi yêu cầu tạo kết quả mới
            const res = await api.post('TestResults/Create', {
                resultSummary: newResultSummary,
                date: newResultDate,
                bookingId: detailData.booking.bookingId
            });

            //  Cố gắng lấy resultId trực tiếp
            let newResultId = res.data?.resultId;

            // Nếu không có resultId => Fallback bằng GetAllPaging
            if (!newResultId || newResultId <= 0) {
                const resResults = await api.get('TestResults/GetAllPaging', {
                    params: { bookingId: detailData.booking.bookingId }
                });

                const resultList = resResults?.data;
                console.log("resResults raw response:", resultList);

                if (!Array.isArray(resultList) || resultList.length === 0) {
                    throw new Error("Không có kết quả nào từ API GetAllPaging.");
                }

                // Dùng Math.max để lấy resultId lớn nhất
                newResultId = Math.max(...resultList.map(r => r.resultId || 0));

                if (!newResultId || newResultId <= 0) {
                    throw new Error("Không tìm thấy resultId hợp lệ từ fallback.");
                }
            }

            // Gán resultId mới vào booking
            const b = detailData.booking;
            await api.put('Bookings/Update', {
                bookingId: b.bookingId,
                userId: b.userId,
                priceId: b.priceId,
                methodId: b.methodId,
                appointmentTime: b.appointmentTime,
                statusId: b.statusId,
                resultId: newResultId
            });

            toast.success("Tạo và gán kết quả thành công!");

            // Cập nhật lại UI
            setDetailData(prev => ({
                ...prev,
                booking: {
                    ...prev.booking,
                    resultId: newResultId,
                    result: {
                        resultId: newResultId,
                        resultSummary: newResultSummary,
                        date: newResultDate
                    }
                }
            }));

            setNewResultSummary('');
            setNewResultDate('');
            setViewMode(true);
            fetchBookingDetail(detailData.booking.bookingId);

        } catch (error) {
            console.error("Lỗi khi tạo/gán kết quả:", {
                message: error.message,
                responseData: error.response?.data,
                status: error.response?.status
            });
            toast.error(`Lỗi khi tạo hoặc gán kết quả: ${error.response?.data?.message || error.message}`);
        } finally {
            setIsLoading(false);
        }
    };



    // const handleUpdateResult = async () => {
    //     try {
    //         await api.put("TestResults/Update", {
    //             ...detailData.booking.result,
    //             resultSummary: detailData.booking.result.resultSummary,
    //             date: detailData.booking.result.date
    //         });
    //         toast.success("Cập nhật kết quả thành công!");
    //         fetchBookingDetail(detailData.booking.bookingId);
    //     } catch (error) {
    //         toast.error("Lỗi cập nhật kết quả!");
    //     }
    // };

    const handleDeleteResult = async () => {
        if (!window.confirm("Bạn có chắc muốn xóa kết quả này không?")) return;
        try {
            await api.delete(`TestResults/DeleteById/${detailData.booking.result.resultId}`);
            await api.put("Bookings/Update", {
                ...detailData.booking
            });
            toast.success("Xóa kết quả thành công!");
            setDetailData(prev => ({
                ...prev,
                booking: {
                    ...prev.booking,
                    resultId: null,
                    result: null
                }
            }));
            setViewMode(false); // Switch back to create mode after deletion
            fetchBookingDetail(detailData.booking.bookingId);
        } catch (err) {
            toast.error("Xóa kết quả thất bại!");
        }
    };

    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary">
                Danh Sách Đặt Chỗ
            </div>

            <div className="row mb-3">
                <div className="col-md-4">
                    <input
                        type="text"
                        placeholder="Tìm......."
                        className="form-control"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>
            </div>

            <table className="table table-bordered table-hover align-middle shadow">
                <thead className="table-primary text-center">
                    <tr>
                        <th>Mã</th>
                        <th>Tên tài khoản</th>
                        <th>Tên Dịch vụ</th>
                        <th>Loại dịch vụ</th>
                        <th>Ngày Đăng Ký</th>
                        <th>Trạng Thái</th>
                        {(isAdmin || isManager || isStaff) && <th>Hành động</th>}
                    </tr>
                </thead>
                <tbody>
                    {isLoading ? (
                        <tr>
                            <td colSpan="7" className="text-center">Đang Tải...</td>
                        </tr>
                    ) : filterBookings.length > 0 ? (
                        filterBookings.map((booking) => (
                            <tr key={booking.bookingId} className="text-center">
                                <td>{booking.bookingId}</td>
                                <td>{getUsername(booking.userId)}</td>
                                <td>{getServiceName(booking.priceId)}</td>
                                <td>{getServiceType(booking.priceId)}</td>
                                <td>{booking.date?.split("T")[0]}</td>
                                <td>{getStatusName(booking.statusId)}</td>
                                <td>
                                    {(isAdmin || isManager) && (
                                        <button className="btn btn-info me-2 shadow" onClick={() => fetchBookingDetail(booking.bookingId)}>
                                            <i className="bi bi-pencil-square"></i>
                                        </button>
                                    )}
                                    {(isAdmin || isManager) && (
                                        <button className="btn btn-danger shadow" onClick={() => fetchDelete(booking.bookingId)}>
                                            <i className="bi bi-trash3-fill"></i>
                                        </button>
                                    )}
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan="7" className="text-center">Không có ai đặt cả.</td>
                        </tr>
                    )}
                </tbody>
            </table>

            {showOverlay && detailData && (
                <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center z-3">
                    <div className="bg-white p-4 rounded shadow-lg overflow-auto" style={{ width: "80%", maxHeight: "90vh" }}>
                        <h4 className="mb-3">Nội dung của đặt chỗ</h4>
                        <div className="accordion" id="accordionBookingDetail">
                            {/* 1. Booking Info */}
                            <div className="accordion-item">
                                <h2 className="accordion-header">
                                    <button className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#bookingInfo">
                                        1. Thông tin đặt chỗ
                                    </button>
                                </h2>
                                <div id="bookingInfo" className="accordion-collapse collapse show">
                                    <div className="accordion-body">
                                        <p><strong>Mã:</strong> {detailData.booking.bookingId}</p>
                                        <p><strong>Dịch vụ:</strong> {detailData.booking.service?.serviceName} - {detailData.booking.service?.serviceType}</p>
                                        <p><strong>Thời gian:</strong> {detailData.booking.duration?.durationName}</p>
                                        <p><strong>Phương Thức thu thập:</strong> {detailData.booking.collectionMethod?.methodName}</p>
                                        <p><strong>Lịch đặt chỗ:</strong> {detailData.booking.appointmentTime}</p>
                                        <div className="mb-3">
                                            <label className="form-label"><strong>Trạng thái:</strong></label>
                                            <select className="form-select" value={detailData.booking.statusId || ''} onChange={(e) => {
                                                setDetailData(prev => ({
                                                    ...prev,
                                                    booking: { ...prev.booking, statusId: parseInt(e.target.value) }
                                                }));
                                            }}>
                                                {dataStatus.map(status => (
                                                    <option key={status.statusId} value={status.statusId}>
                                                        {status.statusName}
                                                    </option>
                                                ))}
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            {/* 2. User Info */}
                            <div className="accordion-item">
                                <h2 className="accordion-header">
                                    <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#userInfo">
                                        2. Thông tin người dùng
                                    </button>
                                </h2>
                                <div id="userInfo" className="accordion-collapse collapse">
                                    <div className="accordion-body">
                                        <p><strong>Họ và Tên:</strong> {detailData.booking.user?.fullName}</p>
                                        <p><strong>Email:</strong> {detailData.booking.user?.email}</p>
                                        <p><strong>Số điện thoại:</strong> {detailData.booking.user?.phone}</p>
                                        <p><strong>CCCD:</strong> {detailData.booking.user?.identifyId}</p>
                                        <p><strong>Vị trí:</strong> {detailData.booking.user?.address}</p>
                                    </div>
                                </div>
                            </div>
                            {/* 3. Patients */}
                            <div className="accordion-item">
                                <h2 className="accordion-header">
                                    <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#patients">
                                        3. Thông tin Khách hàng
                                    </button>
                                </h2>
                                <div id="patients" className="accordion-collapse collapse">
                                    <div className="accordion-body">
                                        {detailData.patients.length > 0 ? (
                                            <table className="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>Họ và Tên</th>
                                                        <th>Ngày Sinh</th>
                                                        <th>Giới tính</th>
                                                        <th>CCCD</th>
                                                        <th>Mẫu Vật</th>
                                                        <th>Mối quan hệ</th>
                                                        <th>Đã từng kiểm tra chưa</th>
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
                                                            { p.hasTestedDna ? (
                                                                <td className="text-success">Có</td>
                                                            ) : (
                                                                <td className="text-danger">Không</td>
                                                            )}
                                                        </tr>
                                                    ))}
                                                </tbody>
                                            </table>
                                        ) : <p>Không có dữ liệu về Khách hàng</p>}
                                    </div>
                                </div>
                            </div>
                            {/* 4. Proccess Test */}
                            <div className="accordion-item">
                                <h2 className="accordion-header">
                                    <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#testProcess">
                                        4. Quá trình kiểm tra
                                    </button>
                                </h2>
                                <div id="testProcess" className="accordion-collapse collapse">
                                    <div className="accordion-body">
                                        {detailData.processes.length > 0 ? (
                                            <>
                                            <table className="table table-bordered">
                                                <thead>
                                                <tr>
                                                    <th>Bước</th>
                                                    <th>Trạng thái</th>
                                                </tr>
                                                </thead>
                                                <tbody>
                                                {detailData.processes.map((p, index) => (
                                                    <tr key={p.processId}>
                                                    <td>{p.step?.stepName}</td>
                                                    <td>
                                                        <select
                                                        className="form-select"
                                                        value={p.status?.statusId || ''}
                                                        onChange={(e) => {
                                                            const updatedProcesses = [...detailData.processes];
                                                            const selectedStatus = dataStatus.find(
                                                            s => s.statusId === parseInt(e.target.value)
                                                            );
                                                            updatedProcesses[index].status = selectedStatus;

                                                            setDetailData(prev => ({
                                                            ...prev,
                                                            processes: updatedProcesses
                                                            }));
                                                        }}
                                                        >
                                                        {dataStatus.map(status => (
                                                            <option key={status.statusId} value={status.statusId}>
                                                            {status.statusName}
                                                            </option>
                                                        ))}
                                                        </select>
                                                    </td>
                                                    </tr>
                                                ))}
                                                </tbody>
                                            </table>

                                            <div className="text-end">
                                                <button
                                                className="btn btn-info me-2"
                                                onClick={async () => {
                                                    try {
                                                    const updatePromises = detailData.processes.map(p => {
                                                        return api.put('TestProcess/Update', {
                                                        processId: p.processId,
                                                        bookingId: detailData.booking.bookingId,
                                                        stepId: p.step?.stepId,
                                                        statusId: p.status?.statusId,
                                                        description: p.description || "",
                                                        updatedAt: new Date().toISOString()
                                                        });
                                                    });

                                                    await Promise.all(updatePromises);
                                                    toast.success("All process steps updated!");
                                                    fetchBookingDetail(detailData.booking.bookingId);
                                                    } catch (error) {
                                                    toast.error("Failed to update process steps");
                                                    console.error(error);
                                                    }
                                                }}
                                                >
                                                Cập nhật tất cả
                                                </button>
                                            </div>
                                            </>
                                        ) : (
                                            <p>Không có quá trình kiểm tra nào</p>
                                        )}
                                    </div>
                                </div>
                            </div>
                            {/* 5. Result */}
                            <div className="accordion-item">
                                <h2 className="accordion-header">
                                    <button
                                    className="accordion-button collapsed"
                                    type="button"
                                    data-bs-toggle="collapse"
                                    data-bs-target="#result"
                                    >
                                    5. Kết quả
                                    </button>
                                </h2>
                                <div id="result" className="accordion-collapse collapse">
                                    <div className="accordion-body">
                                        {viewMode && detailData.booking?.result ? (
                                            <>
                                            <table className="table table-bordered">
                                                <thead>
                                                <tr>
                                                    <th>Kết quả</th>
                                                    <th>Ngày Công Bố</th>
                                                </tr>
                                                </thead>
                                                <tbody>
                                                <tr>
                                                    <td>{detailData.booking.result.resultSummary}</td>
                                                    <td>{detailData.booking.result.date?.split("T")[0] || ""}</td>
                                                </tr>
                                                </tbody>
                                            </table>

                                            <div className="text-end">
                                                <button
                                                className="btn btn-warning me-2"
                                                onClick={() => setViewMode(false)}
                                                >
                                                Chỉnh sửa
                                                </button>
                                                <button
                                                className="btn btn-danger"
                                                onClick={handleDeleteResult}
                                                >
                                                Xóa Kết quả
                                                </button>
                                            </div>
                                            </>
                                        ) : (
                                            <>
                                            <div className="mb-3">
                                                <label>Kết quả Tóm tắt:</label>
                                                <input
                                                    className="form-control"
                                                    value={newResultSummary}
                                                    onChange={(e) => setNewResultSummary(e.target.value)}
                                                />
                                            </div>
                                            <div className="mb-3">
                                                <label>Ngày Công bố:</label>
                                                <input
                                                    type="date"
                                                    className="form-control"
                                                    value={newResultDate}
                                                    onChange={(e) => setNewResultDate(e.target.value)}
                                                />
                                            </div>
                                            <div className="text-end">
                                                <button
                                                    className="btn btn-success"
                                                    onClick={handleCreateResult}
                                                >
                                                    Tạo kết quả
                                                </button>
                                            </div>
                                            </>
                                        )}
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div className="text-end mt-3">
                            <button className="btn btn-primary me-2" onClick={async () => {
                                try {
                                    await api.put(`Bookings/Update`, detailData.booking);
                                    toast.success("Booking status updated!");
                                    setShowOverlay(false);
                                    fetchData();
                                } catch (err) {
                                    toast.error("Thất bại cập nhật trạng thái đặt chỗ.");
                                }
                            }}>
                                Cập nhật Trạng thái
                            </button>
                            <button className="btn btn-danger" onClick={() => setShowOverlay(false)}>Đóng</button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default Booking;