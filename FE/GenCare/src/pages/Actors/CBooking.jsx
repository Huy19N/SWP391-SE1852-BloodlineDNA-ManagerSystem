import React, { useEffect, useState } from "react";
import api from '../../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';


function Booking(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataUser, setDataUser] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);
    const [showOverlay, setShowOverlay] = useState(false);
    const [detailData, setDetailData] = useState(null);
    const [showUpdateResult, setShowUpdateResult] = useState(false);
    const [editBookingResult, setEditBookingResult] = useState(null);
    const [search,setSearch] = useState('');

    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';
    
    

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const [resBooking, resUser, resService, resStatus] = await Promise.all([
                api.get('Bookings/GetAll'),
                api.get('Users/GetAll'),
                api.get('Services/GetAllPaging'),
                api.get('Status/GetAllStatus'),
            ]);
            
            
            // lưu status id vào local 
            const dataStatus = resStatus.data.data;
            const statusId_Approve = dataStatus.find(s => s.statusId === 6)?.statusId;
            const statusId_NotPay = dataStatus.find(s => s.statusId === 1)?.statusId;
            const statusId_Notdo = dataStatus.find(s => s.statusId === 2)?.statusId;
            localStorage.setItem('statusId_Approve', statusId_Approve);
            localStorage.setItem('statusId_NotPay', statusId_NotPay);
            localStorage.setItem('statusId_Notdo', statusId_Notdo);
            
            

            setDataBooking(resBooking.data.data);
            setDataUser(resUser.data.data);
            setDataService(resService.data.data);
            setDataStatus(resStatus.data.data);
            
            
        }
        catch(error){
            toast.error("Error Data!");
        }
        finally{
            setIsLoading(false);
        }
    }

    //Delete 
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

    //booking details
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

        const bookingList = resBooking.data.data;

        const user = resUserAll.data.data.find(u => u.userId === bookingList.userId);
        const service = resServiceAll.data.data.find(s => s.serviceId === bookingList.serviceId);
        const status = resStatusAll.data.data.find(s => s.statusId === bookingList.statusId);
        const method = resMethodAll.data.data.find(m => m.methodId === bookingList.methodId);
        const duration = resDurationAll.data.data.find(d => d.durationId === bookingList.durationId)
        const result = resResults.data.find(r => r.resultId === bookingList.resultId);
        


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
    } catch (error) {
        toast.error("Failed to load booking detail");
        console.error("Detail fetch error:", error);
    }
};

    useEffect (() => {
        fetchData();
    }, []);

    const getUsername = (userId) => {
        const user = dataUser.find(u => u.userId === userId);
        return user ? user.fullName : 'Empty';
    };

    const getServiceName = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceName : 'Empty';
    };

    const getServiceType = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceType : 'Empty';
    };

    const getStatusName = (statusId) => {
        const status = dataStatus.find(u => u.statusId === statusId);
        return status ? status.statusName : 'Empty';
    };

    const statusID_Approve = parseInt(localStorage.getItem('statusId_Approve') || 6);
    const statusID_NotPay = parseInt(localStorage.getItem('statusId_NotPay') || 1);
    const statusID_NotDo = parseInt(localStorage.getItem('statusId_Notdo') || 2);
    const filterBookings = dataBooking.filter((bookings) => {
        const keyword = search.toLowerCase();
        return(
                bookings.statusId !== statusID_NotPay && bookings.statusId !== statusID_NotDo && bookings.statusId !== statusID_Approve && (
                bookings.bookingId.toString().includes(keyword) ||
                getUsername(bookings.userId).toLowerCase().includes(keyword) ||
                getServiceName(bookings.serviceId).toLowerCase().includes(keyword) ||
                getStatusName(bookings.statusId).toLowerCase().includes(keyword) ||
                bookings.date?.split("T")[0].toString().includes(keyword) ||
                getServiceType(bookings.serviceId).toLowerCase().includes(keyword)
            )
        );
    });
    
    const handleShowUpdateResult = async (booking) => {
        try {
            const res = await api.get(`Bookings/GetById/${booking.bookingId}`);
            const fullBooking = res.data.data;

            setEditBookingResult({
                ...fullBooking,
                resultId: booking.resultId || ''
            });

            setShowUpdateResult(true);
        } catch (error) {
            toast.error("Failed to load booking data.");
        }
    };

    console.log(detailData);

    return (
            <div className="container mt-5">
                <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary">
                    Booking List
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
                            <th>ID</th>
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
                                    <td>{getServiceName(booking.serviceId)}</td>
                                    <td>{getServiceType(booking.serviceId)}</td>
                                    <td>{booking.date?.split("T")[0]}</td>
                                    <td>{getStatusName(booking.statusId)}</td>
                                    <td>
                                        {(isAdmin || isManager) && (
                                            <button className="btn btn-info me-2 shadow" onClick={() => fetchBookingDetail(booking.bookingId)}>
                                            <i className="bi bi-pencil-square"></i>
                                            </button>
                                        )}
                                        
                                        {(isAdmin || isManager || isStaff) && (
                                            <button className="btn btn-success me-2 shadow" onClick={() => handleShowUpdateResult(booking)}>
                                            <i className="bi bi-clipboard2-check-fill"></i>
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
                
                {/* Xem chi tiết Form Booking */}
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
                                            <p><strong>ID:</strong> {detailData.booking.bookingId}</p>
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

                                {/* 3. Customer */}
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
                                                            <th>Mẫu Vật</th> {/* Thay vì Sample Type */}
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
                                {/* 4. Process Test */}
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

                                        {/* Nút cập nhật tất cả các status */}
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
                                            <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#result">
                                                5. Kết quả
                                            </button>
                                        </h2>
                                        <div id="result" className="accordion-collapse collapse">
                                            <div className="accordion-body">
                                                {detailData.booking.result ? (
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
                                                                <td>{detailData.booking.result.date?.split("T")[0]}</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                ) : <p>Không có dữ liệu</p>}
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
                                        toast.error("Failed to update booking status.");
                                    }
                                }}>
                                    Cập nhật Trạng thái
                                </button>
                                <button className="btn btn-danger" onClick={() => setShowOverlay(false)}>Đóng</button>
                            </div>
                        </div>
                    </div>
                )}
                

                {showUpdateResult && (
                    <div className="update-overlay">
                        <div className="update-box">
                            <h5 className="text-center">Cập Nhật Kết Quả DNA</h5>
                            <form onSubmit={async (e) => {
                                e.preventDefault();
                                try {
                                    await api.put('Bookings/Update', editBookingResult);
                                    toast.success("Result updated for booking!");
                                    setShowUpdateResult(false);
                                    fetchData();
                                } catch (err) {
                                    toast.error("Update failed!");
                                }
                            }}>
                                <div className="mb-3">
                                    <label>ID của Đặt Chỗ:</label>
                                    <input className="form-control" value={editBookingResult.bookingId} readOnly />
                                </div>
                                <div className="mb-3">
                                    <label>ID của Kết Quả:</label>
                                    <input
                                        type="number"
                                        className="form-control"
                                        value={editBookingResult.resultId}
                                        onChange={(e) =>
                                            setEditBookingResult({
                                                ...editBookingResult,
                                                resultId: parseInt(e.target.value)
                                            })
                                        }
                                        required
                                    />
                                </div>
                                <div className="text-end">
                                    <button type="submit" className="btn btn-success me-2">Lưu</button>
                                    <button className="btn btn-danger" onClick={() => setShowUpdateResult(false)}>Hủy</button>
                                </div>
                            </form>
                        </div>
                    </div>
                )}
            </div>
        
    );
}

export default Booking;