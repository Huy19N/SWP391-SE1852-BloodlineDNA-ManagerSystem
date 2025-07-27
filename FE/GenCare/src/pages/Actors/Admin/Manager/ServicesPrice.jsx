import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function ServicesPrice(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataServicesPrice, setDataServicesPrice] = useState([]);
    const [dataService, setDataService] = useState([]);
    const [dataDuration, setDataDuration] = useState([]);
    const [search,setSearch] = useState('');
    const [editServicesPrice,setEditServicesPrice] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [fromDataServicesPrice, setFromDataServicesPrice] = useState({
        serviceId: '',
        durationId: '',
        price: '',
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async () => {
        setIsLoading(true);
        try {
            const [resServicePrices, resService, resDuration] = await Promise.all([
                api.get('ServicePrices/GetAllPaging', {
                    params: {
                        page: currentPage
                    }
                }),
                api.get('Services/GetAllPaging'),
                api.get('Durations/GetAllPaging')
            ]);

            const dataServicesPrice = resServicePrices.data.data;
            const totalPageFromApi = resServicePrices.data.page || 1;
            console.log("totalPage", totalPageFromApi);

            setDataServicesPrice(dataServicesPrice);
            setDataService(resService.data.data);
            setDataDuration(resDuration.data.data);
            setTotalPages(totalPageFromApi);
        } catch (error) {
            toast.error("Thất bại tải dữ liệu Services Price!");
        } finally {
            setIsLoading(false);
        }
    };

    //API gọi delete ServicesPrice
    const fetchDelete = async (priceId) => {
        if(!window.confirm("Bạn muốn xóa giá của dịch vụ này đúng không?")) return;

        try{
            await api.delete(`ServicePrices/DeleteById/${priceId}`);
            toast.success("Xóa giá dịch vụ này thành công!");

            fetchData();
        }
        catch (error){
            console.log("Xóa lỗi", error);
            toast.error("Thất bại xóa giá của dịch vụ này!")
        }
    }

    useEffect (() => {
        fetchData();
    }, [currentPage, search]);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataServicesPrice(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('ServicePrices/Create', fromDataServicesPrice);
            toast.success("Giá dịch vụ này tạo thành công!");
            setShowCreateModal(false);
            setFromDataServicesPrice({ serviceId: '', durationId: '', price: ''});
            fetchData();
        } catch (error) {
            console.error("Lỗi tạo ServicePrice", error);
            toast.error("Thất bại tạo giá dịch vụ này!");
        }
    };

    const getServiceName = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceName : 'Trống';
    };

    const getServiceType = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceType : 'Trống';
    };

    const getDuration = (durationId) => {
        const duration = dataDuration.find(d => d.durationId === durationId);
        return duration ? duration.durationName : 'Trống';
    };



    //Filter 
    const filteredServicePrices = dataServicesPrice.filter((price) => {
        const keyword = search.toLowerCase();
        return (
            price.priceId.toString().includes(keyword) ||
            getServiceName(price.serviceId).toLowerCase().includes(keyword) || 
            getServiceType(price.serviceId).toLowerCase().includes(keyword) ||
            getDuration(price.durationId).toLowerCase().includes(keyword) ||
            price.price.toString().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Danh sách giá dịch vụ
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
                        Thêm mới giá dịch vụ
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>Mã</th>
                    <th>Tên Dịch Vụ</th>
                    <th>Loại Dịch Vụ</th>
                    <th>Thời Lượng</th>
                    <th>Giá</th>
                    <th>Trạng Thái</th>
                    {(isAdmin || isManager) ? <th>Hành Động</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="6" className="text-center">Tải...</td>
                    </tr>
                ) : filteredServicePrices.length > 0 ? (
                    filteredServicePrices.map((price) => (
                    <tr key={price.priceId}>
                        <td>{price.priceId}</td>
                        <td>{getServiceName(price.serviceId)}</td>
                        <td>{getServiceType(price.serviceId)}</td>
                        <td>{getDuration(price.durationId)}</td>
                        <td>{price.price || 'trống'}</td>
                        <td>{price.isDeleted ? "Đã xóa" : "Còn hoạt động"}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditServicesPrice(price)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update Services price*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(price.priceId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa Service priceId*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="6" className="text-center">Không có Giá Của Dịch Vụ nào.</td>
                    </tr>
                )}
                </tbody>
            </table>

            <div className="d-flex justify-content-center align-items-center mt-3">
                <button
                    className="btn btn-secondary me-2"
                    onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
                    disabled={currentPage === 1}
                >
                    Trang trước
                </button>
                <span>Trang {currentPage} / {totalPages}</span>
                <button
                    className="btn btn-secondary ms-2"
                    onClick={() => setCurrentPage((prev) => Math.min(prev + 1, totalPages))}
                    disabled={currentPage === totalPages}
                >
                    Trang sau
                </button>
            </div>

            {/* Thêm service price*/}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Tạo Giá Mới</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Mã Dịch Vụ: </label>
                                <input
                                    type="number"
                                    name="serviceId"
                                    className="form-control"
                                    value={fromDataServicesPrice.serviceId}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Mã Thời Lượng: </label>
                                <input
                                    type="number"
                                    name="durationId"
                                    className="form-control"
                                    value={fromDataServicesPrice.durationId}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Giá: </label>
                                <input
                                    type="number"
                                    name="price"
                                    className="form-control"
                                    value={fromDataServicesPrice.price}
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
            {editServicesPrice && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
                <h5>Mã Giá: {editServicesPrice.priceId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`ServicePrices/Update`, editServicesPrice);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditServicesPrice(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Mã Dịch Vụ: </label>
                    <input
                    type="number"
                    className="form-control"
                    value={editServicesPrice.serviceId}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditServicesPrice({ ...editServicesPrice, serviceId: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Mã Thời Lượng: </label>
                    <input
                    type="number"
                    className="form-control"
                    value={editServicesPrice.durationId}
                    onChange={(e) =>
                        setEditServicesPrice({ ...editServicesPrice, durationId: parseInt(e.target.value) })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Giá: </label>
                    <input
                    type="number"
                    className="form-control"
                    value={editServicesPrice.price}
                    onChange={(e) =>
                        setEditServicesPrice({ ...editServicesPrice, price: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Trạng Thái: </label>
                    <select
                        className="form-control"
                        value={editServicesPrice.isDeleted ? "true" : "false"}
                        onChange={(e) =>
                        setEditServicesPrice({
                            ...editServicesPrice,
                            isDeleted: e.target.value === "true",
                        })
                        }
                    >
                        <option value="false">Còn hoạt động</option>
                        <option value="true">Đã xóa</option>
                    </select>
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Lưu
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditServicesPrice(null)}
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

export default ServicesPrice;