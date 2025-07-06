import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Service(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataService, setDataService] = useState([]);
    const [search,setSearch] = useState('');
    const [editService,setEditService] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataService, setFromDataService] = useState({
        serviceName: '',
        serviceType: '',
        description: ''
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resService = await api.get('Services/GetAllPaging');
            const dataService = resService.data.data;

            console.log("Service", dataService);

            setDataService(dataService);
        }
        catch(error){
            console.log("Error DataService", error.dataService);
            toast.error("Failed Loading Data Service");
        }
        finally{
            setIsLoading(false);
        }
    }

            //API gọi delete services
    const fetchDelete = async (serviceId) => {
        if(!window.confirm("Are you sure you want to delete this service?")) return;

        try{
            await api.delete(`Services/DeleteById/${serviceId}`);
            toast.success("This user deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete service this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataService(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('/Services/Create', fromDataService);
            toast.success("Service created successfully!");
            setShowCreateModal(false);
            setFromDataService({ serviceName: '', serviceType: '', description: '' });
            fetchData();
        } catch (error) {
            console.error("Error creating service", error);
            toast.error("Failed to create service");
        }
    };



    //Filter 
    const filteredService = dataService.filter((service) => {
        const keyword = search.toLowerCase();
        return (
            service.serviceId.toString().includes(keyword) ||
            service.serviceName.toLowerCase().includes(keyword) || 
            service.serviceType.toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Services
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
                        Add New Service
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Name Service</th>
                    <th>Type Service</th>
                    <th>description</th>
                    {isAdmin ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="5" className="text-center">Loading...</td>
                    </tr>
                ) : filteredService.length > 0 ? (
                    filteredService.map((service) => (
                    <tr key={service.serviceId}>
                        <td>{service.serviceId}</td>
                        <td>{service.serviceName}</td>
                        <td>{service.serviceType}</td>
                        <td>{service.description || 'Empty'}</td>
                        {isAdmin || isManager ? 
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditService(service)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update user*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(service.serviceId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa user*/}
                        </td>
                        : null}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">No users found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm service */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New Service</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Service Name</label>
                                <input
                                    type="text"
                                    name="serviceName"
                                    className="form-control"
                                    value={fromDataService.serviceName}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Service Type</label>
                                <input
                                    type="text"
                                    name="serviceType"
                                    className="form-control"
                                    value={fromDataService.serviceType}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Description</label>
                                <textarea
                                    name="description"
                                    className="form-control"
                                    value={fromDataService.description}
                                    onChange={handleCreateChange}
                                    rows={3}
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
            {editService && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>Service ID: {editService.serviceId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`Services/Update`, editService);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditService(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Name:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editService.serviceName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditService({ ...editService, serviceName: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Service Type:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editService.serviceType}
                    onChange={(e) =>
                        setEditService({ ...editService, serviceType: parseInt(e.target.value) })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Description:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editService.description}
                    onChange={(e) =>
                        setEditService({ ...editService, description: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditService(null)}
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

export default Service;