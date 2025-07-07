import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Status(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataStatus, setDataStatus] = useState([]);
    const [search,setSearch] = useState('');
    const [editStatus,setEditStatus] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataStatus, setFromDataStatus] = useState({
        statusName: '',
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resStatus = await api.get('Status/GetAllStatus');
            const dataStatus = resStatus.data.data;

            console.log("Status", dataStatus);

            setDataStatus(dataStatus);
        }
        catch(error){
            console.log("Error dataStatus", error.dataStatus);
            toast.error("Failed Loading Data Status");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete Status
    const fetchDelete = async (statusId) => {
        if(!window.confirm("Are you sure you want to delete this Status?")) return;

        try{
            await api.delete(`Status/DeleteById/${statusId}`);
            toast.success("This Status deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Status this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataStatus(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('Status/Create', fromDataStatus);
            toast.success("Status created successfully!");
            setShowCreateModal(false);
            setFromDataStatus({ statusName: ''});
            fetchData();
        } catch (error) {
            console.error("Error creating Status", error);
            toast.error("Failed to create Status");
        }
    };



    //Filter 
    const filteredStatus = dataStatus.filter((status) => {
        const keyword = search.toLowerCase();
        return (
            status.statusId.toString().includes(keyword) ||
            status.statusName.toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Status List
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
                        Add New Status
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Name Status</th>
                    {isAdmin ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="3" className="text-center">Loading...</td>
                    </tr>
                ) : filteredStatus.length > 0 ? (
                    filteredStatus.map((status) => (
                    <tr key={status.statusId}>
                        <td>{status.statusId}</td>
                        <td>{status.statusName}</td>
                        {isAdmin || isManager ? 
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditStatus(status)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update Status*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(status.statusId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa Status*/}
                        </td>
                        : null}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">No Status found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm Status */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New Status</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Status Name</label>
                                <input
                                    type="text"
                                    name="statusName"
                                    className="form-control"
                                    value={fromDataStatus.statusName}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>

                            <div className="text-end">
                                <button type="button" className="btn btn-secondary me-2" onClick={() => setShowCreateModal(false)}>Cancel</button>
                                <button type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}


            {/*Update here nha */}
            {editStatus && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>Status ID: {editStatus.statusId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`Status/Update`, editStatus);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditStatus(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Status Name:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editStatus.statusName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditStatus({ ...editStatus, statusName: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditStatus(null)}
                >
                    Cancel
                </button>
                </form>
            </div>
            </div>
            )}
        </div>
    );
}

export default Status;