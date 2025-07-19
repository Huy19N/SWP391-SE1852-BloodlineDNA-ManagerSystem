import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';


function CollectionMethod(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataCollect, setDataCollect] = useState([]);
    const [search,setSearch] = useState('');
    const [editCollection,setEditCollection] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataCollection, setFromDataCollection] = useState({
        methodName: ''
    });
    
    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resCollection = await api.get('CollectionMethod/GetAll');
            const dataCollection = resCollection.data.data;

            console.log("Collection", dataCollection);

            setDataCollect(dataCollection);
        }
        catch(error){
            console.log("Error dataCollection", error.dataCollection);
            toast.error("Failed Loading data collection");
        }
        finally{
            setIsLoading(false);
        }
    }


    //API gọi delete Collection
    const fetchDelete = async (methodId) => {
        if(!window.confirm("Are you sure you want to delete this Collection?")) return;

        try{
            await api.delete(`CollectionMethod/DeleteById/${methodId}`);
            toast.success("This user deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Collection this!")
        }
    }

    useEffect (() => {
            fetchData();
    }, []);


    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataCollection(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('CollectionMethod/Create', fromDataCollection);
            toast.success("Collection created successfully!");
            setShowCreateModal(false);
            setFromDataCollection({ methodName: ''});
            fetchData();
        } catch (error) {
            console.error("Error creating Collection", error);
            toast.error("Failed to create Collection");
        }
    };
    
    //Filter 
    const filteredCollection = dataCollect.filter((collection) => {
        const keyword = search.toLowerCase();
        return (
            collection.methodId.toString().includes(keyword) ||
            collection.methodName.toLowerCase().includes(keyword)
        );
    });


    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Phương Thức Xét Nghiệm
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
                        Thêm Phương Thức Mới
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
                ) : filteredCollection.length > 0 ? (
                    filteredCollection.map((collection) => (
                    <tr key={collection.methodId}>
                        <td>{collection.methodId}</td>
                        <td>{collection.methodName}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditCollection(collection)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update collection*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(collection.methodId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa collection*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">Không Có Pương Thức.</td>
                    </tr>
                )}
                </tbody>
            </table>

            {/* Thêm collection */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Tạo Thêm Phương Thức</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Tên Dịch Vụ</label>
                                <input
                                    type="text"
                                    name="methodName"
                                    className="form-control"
                                    value={fromDataCollection.methodName}
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
            {editCollection && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
                <h5>ID: {editCollection.methodId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`CollectionMethod/Update`, editCollection);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditCollection(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Tên Phương Thức:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editCollection.methodName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditService({ ...editCollection, methodName: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Lưu
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditCollection(null)}
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

export default CollectionMethod;