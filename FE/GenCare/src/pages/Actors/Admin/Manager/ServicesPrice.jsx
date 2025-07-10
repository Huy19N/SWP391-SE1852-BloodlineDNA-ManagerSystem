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
    const [fromDataServicesPrice, setFromDataServicesPrice] = useState({
        serviceId: '',
        durationId: '',
        price: ''
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const [resServicePrices, resService, resDuration] = await Promise.all([
                 api.get('ServicePrices/GetAllPaging'),
                 api.get('Services/GetAllPaging'),
                 api.get(`Durations/GetAllPaging`)
            ]);
            const dataServicesPrice = resServicePrices.data.data;
            const dataService = resService.data.data;
            const dataDuration = resDuration.data.data;

            console.log("ServicesPrice", dataServicesPrice);
            console.log("dataService", dataService);
            console.log("dataDuration", dataDuration);

            setDataServicesPrice(dataServicesPrice);
            setDataService(dataService);
            setDataDuration(dataDuration);

        }
        catch(error){
            console.log("Error DataServicesPrice", error.dataServicesPrice);
            console.log("Error Data Service", error.dataService);
            console.log("Error Data Duration", error.dataDuration);
            toast.error("Failed Loading Data ServicesPrice");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete ServicesPrice
    const fetchDelete = async (priceId) => {
        if(!window.confirm("Are you sure you want to delete this Services Price?")) return;

        try{
            await api.delete(`ServicePrices/DeleteById/${priceId}`);
            toast.success("This user deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Services Price this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataServicesPrice(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('ServicePrices/Create', fromDataServicesPrice);
            toast.success("Service created successfully!");
            setShowCreateModal(false);
            setFromDataServicesPrice({ serviceId: '', durationId: '', price: '' });
            fetchData();
        } catch (error) {
            console.error("Error creating Service Prices", error);
            toast.error("Failed to create Service Prices");
        }
    };

    const getServiceName = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceName : 'Empty';
    };

    const getServiceType = (serviceId) => {
        const service = dataService.find(u => u.serviceId === serviceId);
        return service ? service.serviceType : 'Empty';
    };

    const getDuration = (durationId) => {
        const duration = dataDuration.find(d => d.durationId === durationId);
        return duration ? duration.durationName : 'Empty';
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
                Service Prices List
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
                        Add New Services Prices
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Name Service</th>
                    <th>Type Service</th>
                    <th>Name Duration</th>
                    <th>Price</th>
                    {(isAdmin || isManager) ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="6" className="text-center">Loading...</td>
                    </tr>
                ) : filteredServicePrices.length > 0 ? (
                    filteredServicePrices.map((price) => (
                    <tr key={price.priceId}>
                        <td>{price.priceId}</td>
                        <td>{getServiceName(price.serviceId)}</td>
                        <td>{getServiceType(price.serviceId)}</td>
                        <td>{getDuration(price.durationId)}</td>
                        <td>{price.price || 'Empty'}</td>
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
                    <td colSpan="6" className="text-center">No Services Price found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm service price*/}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New Services Price</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">ServiceID: </label>
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
                                <label className="form-label">DurationID: </label>
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
                                <label className="form-label">Price: </label>
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
                                <button type="button" className="btn btn-secondary me-2" onClick={() => setShowCreateModal(false)}>Cancel</button>
                                <button type="submit" className="btn btn-primary">Create</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}


            {/*Update here nha */}
            {editServicesPrice && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>PriceID: {editServicesPrice.priceId}</h5>
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
                    <label>ServiceID: </label>
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
                    <label>DurationID: </label>
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
                    <label>Price: </label>
                    <input
                    type="number"
                    className="form-control"
                    value={editServicesPrice.price}
                    onChange={(e) =>
                        setEditServicesPrice({ ...editServicesPrice, price: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditServicesPrice(null)}
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

export default ServicesPrice;