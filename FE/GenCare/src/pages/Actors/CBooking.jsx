import React, { useEffect, useState } from "react";
import api from '../../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';


function Booking(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataUser, setDataUser] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataStatus, setDataStatus] = useState([]);

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

    return (
            <div className="container mt-5">
                <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary">
                    Booking List
                </div>

                <table className="table table-bordered table-hover align-middle">
                    <thead className="table-primary text-center">
                        <tr>
                            <th>ID</th>
                            <th>User Name</th>
                            <th>Service Name</th>
                            <th>Service Type</th>
                            <th>Date</th>
                            <th>Status</th>
                            {isAdmin && <th>Action</th>}
                        </tr>
                    </thead>
                    <tbody>
                        {isLoading ? (
                            <tr>
                                <td colSpan="7" className="text-center">Loading...</td>
                            </tr>
                        ) : dataBooking.length > 0 ? (
                            dataBooking.map((booking) => (
                                <tr key={booking.bookingId} className="text-center">
                                    <td>{booking.bookingId}</td>
                                    <td>{getUsername(booking.userId)}</td>
                                    <td>{getServiceName(booking.serviceId)}</td>
                                    <td>{getServiceType(booking.serviceId)}</td>
                                    <td>{booking.date?.split("T")[0]}</td>
                                    <td>{getStatusName(booking.statusId)}</td>
                                    {isAdmin && (
                                        <td>
                                            <button className="btn btn-info me-2">
                                                <i className="bi bi-pencil-square"></i>
                                            </button>
                                            <button className="btn btn-danger">
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
                <ToastContainer />
            </div>
        
    );
}

export default Booking;