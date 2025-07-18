import React, { useEffect, useState } from "react";
import api from '../../../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';

function Approve(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataUser, setDataUser] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);
    const [showOverlay, setShowOverlay] = useState(false);
    const [detailData, setDetailData] = useState(null);
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
            const statusId = dataStatus.find(s => s.statusId === 6)?.statusId;
            localStorage.setItem('statusId_Approve', statusId);
            
            

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
            resServiceAll,
            resStatusAll,
            resDurationAll,
            resMethodAll,
        ] = await Promise.all([
            api.get(`Bookings/GetById/${bookingId}`),
            api.get(`Users/GetAll`),
            api.get(`Patient/GetAll`),
            api.get(`Samples/GetAllPaging`),
            api.get(`Services/GetAllPaging`),
            api.get(`Status/GetAllStatus`),
            api.get(`Durations/GetAllPaging`),
            api.get(`CollectionMethod/GetAll`),
        ]);

        const bookingList = resBooking.data.data;

        const user = resUserAll.data.data.find(u => u.userId === bookingList.userId);
        const service = resServiceAll.data.data.find(s => s.serviceId === bookingList.serviceId);
        const status = resStatusAll.data.data.find(s => s.statusId === bookingList.statusId);
        const method = resMethodAll.data.data.find(m => m.methodId === bookingList.methodId);

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

        setDetailData({
            booking: {
                ...bookingList,
                user,
                service,
                status,
                duration: resDurationAll.data.data.find(d => d.durationId === bookingList.durationId),
                collectionMethod: method
            },
            patients,
            samples: resSampleAll.data.data,
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

    const statusID = parseInt(localStorage.getItem('statusId_Approve') || 6);
    const statusID_NotPay = parseInt(localStorage.getItem('statusId_NotPay') || 1);
    const filterBookings = dataBooking.filter((bookings) => {
        const keyword = search.toLowerCase();
        return(
                bookings.statusId === statusID && (
                bookings.bookingId.toString().includes(keyword) ||
                getUsername(bookings.userId).toLowerCase().includes(keyword) ||
                getServiceName(bookings.serviceId).toLowerCase().includes(keyword) ||
                getStatusName(bookings.statusId).toLowerCase().includes(keyword) ||
                bookings.date?.split("T")[0].toString().includes(keyword) ||
                getServiceType(bookings.serviceId).toLowerCase().includes(keyword)
            )
        );
    });
    console.log("filterStatusID", filterBookings);

    const filterBookingsTest = dataBooking.filter((bookings) => {
        const keyword = search.toLowerCase();
        return(
                bookings.statusId === statusID_NotPay && (
                bookings.bookingId.toString().includes(keyword) ||
                getUsername(bookings.userId).toLowerCase().includes(keyword) ||
                getServiceName(bookings.serviceId).toLowerCase().includes(keyword) ||
                getStatusName(bookings.statusId).toLowerCase().includes(keyword) ||
                bookings.date?.split("T")[0].toString().includes(keyword) ||
                getServiceType(bookings.serviceId).toLowerCase().includes(keyword)
                )
        );
    });

    return(
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary">
                    Form List
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
                </div>

                <div className="table-responsive mb-5">
                    <h5 className="border-bottom border-primary text-center mb-4">Mẫu Đăng Ký Đang Chờ Duyệt</h5>
                    <table className="table table-bordered table-hover align-middle shadow">
                        <thead className="table-primary text-center">
                            <tr>
                                <th>ID</th>
                                <th>User Name</th>
                                <th>Service Name</th>
                                <th>Service Type</th>
                                <th>Date</th>
                                <th>Status</th>
                                {(isAdmin || isManager || isStaff) && <th>Action</th>}
                            </tr>
                        </thead>
                        <tbody>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="7" className="text-center">Loading...</td>
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
                                        {(isAdmin || isManager || isStaff) && (
                                            <td>
                                                <button className="btn btn-info me-2 shadow" onClick={() => fetchBookingDetail(booking.bookingId)}>
                                                    <i className="bi bi-pencil-square"></i>
                                                </button>
                                                <button className="btn btn-danger shadow" onClick={() => fetchDelete(booking.bookingId)}>
                                                    <i className="bi bi-trash3-fill"></i>
                                                </button>
                                            </td>
                                        )}
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="7" className="text-center">No booking data found.</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
                
                <div className="table-responsive mb-5">
                    <h5 className="border-bottom border-primary text-center mb-4">Mẫu Đăng Ký chưa Thanh toán</h5>
                    <table className="table table-bordered table-hover align-middle shadow">
                        <thead className="table-primary text-center">
                            <tr>
                                <th>ID</th>
                                <th>User Name</th>
                                <th>Service Name</th>
                                <th>Service Type</th>
                                <th>Date</th>
                                <th>Status</th>
                                {(isAdmin || isManager || isStaff) && <th>Action</th>}
                            </tr>
                        </thead>
                        <tbody>
                            {isLoading ? (
                                <tr>
                                    <td colSpan="7" className="text-center">Loading...</td>
                                </tr>
                            ) : filterBookingsTest.length > 0 ? (
                                filterBookingsTest.map((booking) => (
                                    <tr key={booking.bookingId} className="text-center">
                                        <td>{booking.bookingId}</td>
                                        <td>{getUsername(booking.userId)}</td>
                                        <td>{getServiceName(booking.serviceId)}</td>
                                        <td>{getServiceType(booking.serviceId)}</td>
                                        <td>{booking.date?.split("T")[0]}</td>
                                        <td>{getStatusName(booking.statusId)}</td>
                                        {(isAdmin || isManager || isStaff) && (
                                            <td>
                                                <button className="btn btn-info me-2 shadow" onClick={() => fetchBookingDetail(booking.bookingId)}>
                                                    <i className="bi bi-pencil-square"></i>
                                                </button>
                                                <button className="btn btn-danger shadow" onClick={() => fetchDelete(booking.bookingId)}>
                                                    <i className="bi bi-trash3-fill"></i>
                                                </button>
                                            </td>
                                        )}
                                    </tr>
                                ))
                            ) : (
                                <tr>
                                    <td colSpan="7" className="text-center">No booking data found.</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>

                {/* Xem chi tiết Form Booking */}
                {showOverlay && detailData && (
                    <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center z-3">
                        <div className="bg-white p-4 rounded shadow-lg overflow-auto" style={{ width: "80%", maxHeight: "90vh" }}>
                            <h4 className="mb-3">Booking Details</h4>
                            <div className="accordion" id="accordionBookingDetail">

                                {/* 1. Booking Info */}
                                <div className="accordion-item">
                                    <h2 className="accordion-header">
                                        <button className="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#bookingInfo">
                                            1. Information Booking
                                        </button>
                                    </h2>
                                    <div id="bookingInfo" className="accordion-collapse collapse show">
                                        <div className="accordion-body">
                                            <p><strong>Booking ID:</strong> {detailData.booking.bookingId}</p>
                                            <p><strong>Service:</strong> {detailData.booking.service?.serviceName} - {detailData.booking.service?.serviceType}</p>
                                            <p><strong>Duration:</strong> {detailData.booking.duration?.durationName}</p>
                                            <p><strong>Collection Method:</strong> {detailData.booking.collectionMethod?.methodName}</p>
                                            <p><strong>Appointment:</strong> {detailData.booking.appointmentTime}</p>
                                            <div className="mb-3">
                                                <label className="form-label"><strong>Status:</strong></label>
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
                                            2. Information User 
                                        </button>
                                    </h2>
                                    <div id="userInfo" className="accordion-collapse collapse">
                                        <div className="accordion-body">
                                            <p><strong>Full Name:</strong> {detailData.booking.user?.fullName}</p>
                                            <p><strong>Email:</strong> {detailData.booking.user?.email}</p>
                                            <p><strong>Phone:</strong> {detailData.booking.user?.phone}</p>
                                            <p><strong>Identify ID:</strong> {detailData.booking.user?.identifyId}</p>
                                            <p><strong>Address:</strong> {detailData.booking.user?.address}</p>
                                        </div>
                                    </div>
                                </div>

                                {/* 3. Customer */}
                                <div className="accordion-item">
                                    <h2 className="accordion-header">
                                        <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#patients">
                                            3. Information Customer
                                        </button>
                                    </h2>
                                    <div id="patients" className="accordion-collapse collapse">
                                        <div className="accordion-body">
                                            {detailData.patients.length > 0 ? (
                                                <table className="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th>Full Name</th>
                                                            <th>BirthDate</th>
                                                            <th>Gender</th>
                                                            <th>Identify ID</th>
                                                            <th>Sample Name</th> {/* Thay vì Sample Type */}
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
                                                                <td>{p.hasTestedDNA ? 'Yes' : 'No'}</td>
                                                            </tr>
                                                        ))}
                                                    </tbody>

                                                </table>
                                            ) : <p>No patient data</p>}
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
                                    Update Status
                                </button>
                                <button className="btn btn-danger" onClick={() => setShowOverlay(false)}>Close</button>
                            </div>
                        </div>
                    </div>
                )}
        </div>
    );
}

export default Approve;