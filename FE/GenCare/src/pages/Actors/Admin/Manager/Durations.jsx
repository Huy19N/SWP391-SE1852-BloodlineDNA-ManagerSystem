import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Durations(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataDurations, setDataDurations] = useState([]);
    const [search,setSearch] = useState('');
    const [editDurations,setEditDurations] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataDurations, setFromDataDurations] = useState({
        durationName: '',
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resDuration = await api.get('Durations/GetAllPaging');
            const dataDuration = resDuration.data.data;

            console.log("Duration", dataDuration);

            setDataDurations(dataDuration);
        }
        catch(error){
            console.log("Error dataDuration", error.dataDuration);
            toast.error("Failed Loading Data Duration");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete services
    const fetchDelete = async (durationId) => {
        if(!window.confirm("Are you sure you want to delete this Duration?")) return;

        try{
            await api.delete(`Durations/DeleteById/${durationId}`);
            toast.success("This Duration deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Duration this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataDurations(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('Durations/Create', fromDataDurations);
            toast.success("Durations created successfully!");
            setShowCreateModal(false);
            setFromDataDurations({ durationName: ''});
            fetchData();
        } catch (error) {
            console.error("Error creating Durations", error);
            toast.error("Failed to create Durations");
        }
    };



    //Filter 
    const filteredDurations = dataDurations.filter((duration) => {
        const keyword = search.toLowerCase();
        return (
            duration.durationId.toString().includes(keyword) ||
            duration.durationName.toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Thời Lượng
            </div>
            <div className="row mb-3">
                <div className="col-md-4">
                    <input
                        type="text"
                        placeholder="Search Name or Id..."
                        className="form-control"
                        value={search}
                        onChange={(e) => setSearch(e.target.value)}
                    />
                </div>

                <div className="col-md-4">
                    <button className="btn btn-primary" onClick={() => setShowCreateModal(true)}> 
                        Thêm Thời Lượng Mới
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Tên</th>
                    {(isAdmin || isManager) ? <th>Hành Động</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="3" className="text-center">Tải...</td>
                    </tr>
                ) : filteredDurations.length > 0 ? (
                    filteredDurations.map((duration) => (
                    <tr key={duration.durationId}>
                        <td>{duration.durationId}</td>
                        <td>{duration.durationName}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditDurations(duration)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update duration*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(duration.durationId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa duration*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">Không Tìm Thấy Thời Lượng Nào.</td>
                    </tr>
                )}
                </tbody>
            </table>


            {/* Thêm Durations */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Tạo Thêm Thời Lượng</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Tên Thời Lượng</label>
                                <input
                                    type="text"
                                    name="durationName"
                                    className="form-control"
                                    value={fromDataDurations.durationName}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="text-end">
                                <button type="button" className="btn btn-secondary me-2" onClick={() => setShowCreateModal(false)}>Hủy</button>
                                <button type="submit" className="btn btn-primary">Tạo</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}


            {/*Update here nha */}
            {editDurations && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
                <h5>ID: {editDurations.durationId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`Durations/Update`, editDurations);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditDurations(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Tên Thời Lượng:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editDurations.durationName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditDurations({ ...editDurations, durationName: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Lưu
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditDurations(null)}
                >
                    Hủy
                </button>
                </form>
            </div>
            </div>
            )}
        </div>
    );
}

export default Durations;