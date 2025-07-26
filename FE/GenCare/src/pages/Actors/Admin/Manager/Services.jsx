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
            console.log("Lỗi Dữ liệu DATAService", error.dataService);
            toast.error("Thất bại khi tải dữ liệu dịch vụ!");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete services
    const fetchDelete = async (serviceId) => {
        if(!window.confirm("Bạn có muốn xóa dịch vụ này không?")) return;

        try{
            await api.delete(`Services/DeleteById/${serviceId}`);
            toast.success("Dịch vụ này xóa thành công!");

            fetchData();
        }
        catch (error){
            console.log("Xóa lỗi", error);
            toast.error("Thất bại xóa dịch vụ này!")
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
            toast.success("Tạo dịch vụ này thành công!");
            setShowCreateModal(false);
            setFromDataService({ serviceName: '', serviceType: '', description: '' });
            fetchData();
        } catch (error) {
            console.error("Lỗi tạo dịch vụ", error);
            toast.error("Thất bại khi tạo dịch vụ!");
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
                Dịch Vụ 
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

                <div className="col-md-4">
                    <button className="btn btn-primary" onClick={() => setShowCreateModal(true)}> 
                        Tạo Dịch Vụ
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Tên Dịch vụ</th>
                    <th>Loại Dịch Vụ</th>
                    <th>Mô tả</th>
                    {(isAdmin || isManager) ? <th>Hành động</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="5" className="text-center">Tải...</td>
                    </tr>
                ) : filteredService.length > 0 ? (
                    filteredService.map((service) => (
                    <tr key={service.serviceId}>
                        <td>{service.serviceId}</td>
                        <td>{service.serviceName}</td>
                        <td>{service.serviceType}</td>
                        <td>{service.description || 'Empty'}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditService(service)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update Service*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(service.serviceId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa Service*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="5" className="text-center">Không có dịch vụ nào cả.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm service */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Tạo mới dịch vụ</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Tên dịch vụ</label>
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
                                <label className="form-label">Loại dịch vụ</label>
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
                                <label className="form-label">Mô tả</label>
                                <textarea
                                    name="description"
                                    className="form-control"
                                    value={fromDataService.description}
                                    onChange={handleCreateChange}
                                    rows={5}
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
            {editService && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
                <h5>Mã Dịch vụ: {editService.serviceId}</h5>
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
                    <label>Tên Dịch vụ:</label>
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
                    <label>Loại Dich vụ:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editService.serviceType}
                    onChange={(e) =>
                        setEditService({ ...editService, serviceType: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Mô tả:</label>
                    <textarea
                    name="description"
                    className="form-control"
                    value={editService.description}
                    onChange={(e) =>
                        setEditService({ ...editService, description: e.target.value })
                    }
                    rows={5}
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Lưu
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditService(null)}
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

export default Service;