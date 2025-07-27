import React, {useEffect} from "react";
import api from "../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Feedbacks(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataFeedbacks, setDataFeedbacks] = useState([]);
    const [dataUsers, setDataUsers] = useState([]);
    const [dataBooking, setDataBooking] = useState([]);
    const [search,setSearch] = useState('');
    const [detailData, setDetailData] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const [resUser, resFeedbacks, resBooking] = await Promise.all([
                api.get('Users/GetAll'),
                api.get('Feedbacks/GetAll'),
                api.get('Bookings/GetAll')
            ]);

            const dataUser = resUser.data.data;
            const dataFeedback = resFeedbacks.data.data;
            const dataBooking = resBooking.data.data;

            console.log("dataUser", dataUser);
            console.log("dataFeedback", dataFeedback);

            setDataUsers(dataUser);
            setDataFeedbacks(dataFeedback);
            setDataBooking(dataBooking);
        }
        catch(error){

            if (error.response && error.response.status === 404) {
                setDataUsers([]);
                setDataFeedbacks([]);
            }
            console.log("Lỗi  dataUser", error.dataUser);
            console.log("Lỗi dataFeedback", error.dataFeedback);
            toast.error("Thất bại tải dữ liệu phản hồi!");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete Feedback
    const fetchDelete = async (bookingId) => {
        if(!window.confirm("Bạn có chắc là muốn xóa phản hồi này?")) return;

        try{
            await api.delete(`Feedbacks/DeleteFeedbackByBookingId/${bookingId}`);
            toast.success("Xóa phản hồi này thành công!");

            fetchData();
        }
        catch (error){
            console.log("Lỗi Xóa", error);
            toast.error("Thất bại xóa phản hồi này!");
        }
    }

    const fetchFeedbackDetail = async (bookingId) => {
    try {
        const res = await api.get(`Feedbacks/GetFeedbackByBookingId/${bookingId}`);
        const feedback = res.data.data;
        console.log("detail", feedback);
        setDetailData(feedback);
        setShowCreateModal(true);
    } catch (err) {
        toast.error("Thất Bại tải nội dung phản hồi");
        console.error("Feedback detail error", err);
    }
};

    useEffect (() => {
        fetchData();
    }, []);


    const getUsername = (bookingID) => {
        const booking = dataBooking.find(u => u.bookingId === bookingID);
        const user = dataUsers.find(s => s.userId === booking?.userId);
        return user?.fullName || 'Trống';
    };
    
    //Filter 
    const filteredFeedbacks = dataFeedbacks.filter((feedback) => {
        const keyword = search.toLowerCase();
        return (
            feedback.bookingId.toString().includes(keyword) ||
            feedback.rating.toString().includes(keyword) ||
            getUsername(feedback.bookingId).toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Danh sách Phản Hồi Của Khách Hàng
            </div>
            <div className="row mb-3">
                <div className="col-md-4">
                    <input
                        type="text"
                        placeholder="Tìm..."
                        className="form-control"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>Mã</th>
                    <th>Tên Người Dùng</th>
                    <th>Ngày</th>
                    <th>Đáng Giá</th>
                    {(isAdmin || isManager) ? <th>Hành Động</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="7" className="text-center">Tải...</td>
                    </tr>
                ) : filteredFeedbacks.length > 0 ? (
                    filteredFeedbacks.map((feedback) => (
                    <tr key={feedback.bookingId}>
                        <td>{feedback.bookingId}</td>
                        <td>{getUsername(feedback.bookingId)}</td>
                        <td>{feedback.createdAt?.split("T")[0]}</td>
                        <td>{feedback.rating}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => fetchFeedbackDetail(feedback.bookingId)}>
                            <i className="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update feedback*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(feedback.bookingId)}>
                            <i className="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa feedback*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="7" className="text-center">Không Có Phản Hồi Nào.</td>
                    </tr>
                )}
                </tbody>
            </table>

            {showCreateModal && detailData && (
            <div className="position-fixed top-0 start-0 w-100 h-100 bg-dark bg-opacity-50 d-flex justify-content-center align-items-center z-3">
                <div className="bg-white p-4 rounded shadow-lg" style={{ width: "600px", maxHeight: "90vh" }}>
                <h4 className="mb-4 text-center text-primary border-bottom pb-2">
                    Nội Dung Phản Hồi 
                </h4>

                <div className="mb-3">
                    <label className="form-label"><strong>Mã</strong></label>
                    <input type="text" className="form-control" readOnly value={detailData.bookingId} />
                </div>

                <div className="mb-3">
                    <label className="form-label"><strong>Người Dùng</strong></label>
                    <input type="text" className="form-control" readOnly value={getUsername(detailData.bookingId)} />
                </div>

                <div className="mb-3">
                    <label className="form-label"><strong>Ngày</strong></label>
                    <input type="text" className="form-control" readOnly value={detailData.createdAt?.split("T")[0]} />
                </div>

                <div className="mb-3">
                    <label className="form-label"><strong>Đáng Giá</strong></label>
                    <input type="number" className="form-control" readOnly value={detailData.rating} />
                </div>

                <div className="mb-3">
                    <label className="form-label"><strong>Bình Luận</strong></label>
                    <textarea className="form-control" rows="8" readOnly value={detailData.comment || "No comment"} />
                </div>

                <div className="text-end">
                    <button className="btn btn-secondary" onClick={() => setShowCreateModal(false)}>Đóng</button>
                </div>
                </div>
            </div>
            )}
        </div>
    );
}

export default Feedbacks;