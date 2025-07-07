import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Samples(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataSamples, setDataSamples] = useState([]);
    const [search,setSearch] = useState('');
    const [editSamples,setEditSamples] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataSamples, setFromDataSamples] = useState({
        sampleName: '',
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resSamples = await api.get('Samples/GetAllPaging');
            const dataSamples = resSamples.data.data;

            console.log("Samples", dataSamples);

            setDataSamples(dataSamples);
        }
        catch(error){
            console.log("Error dataSamples", error.dataSamples);
            toast.error("Failed Loading Data Samples");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete samples
    const fetchDelete = async (sampleId) => {
        if(!window.confirm("Are you sure you want to delete this samples?")) return;

        try{
            await api.delete(`Samples/DeleteById/${sampleId}`);
            toast.success("This samples deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete samples this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataSamples(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('Samples/Create', fromDataSamples);
            toast.success("Samples created successfully!");
            setShowCreateModal(false);
            setFromDataSamples({ sampleName: '' });
            fetchData();
        } catch (error) {
            console.error("Error creating Samples", error);
            toast.error("Failed to create Samples");
        }
    };



    //Filter 
    const filteredSamples = dataSamples.filter((sample) => {
        const keyword = search.toLowerCase();
        return (
            sample.sampleId.toString().includes(keyword) ||
            sample.sampleName.toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Samples
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
                        Add New Samples
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Name Samples</th>
                    {isAdmin ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="3" className="text-center">Loading...</td>
                    </tr>
                ) : filteredSamples.length > 0 ? (
                    filteredSamples.map((sample) => (
                    <tr key={sample.sampleId}>
                        <td>{sample.sampleId}</td>
                        <td>{sample.sampleName}</td>
                        {isAdmin || isManager ? 
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditSamples(sample)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update Samples*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(sample.sampleId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa Samples*/}
                        </td>
                        : null}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">No Samples found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm Samples */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New Samples</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Samples Name</label>
                                <input
                                    type="text"
                                    name="sampleName"
                                    className="form-control"
                                    value={fromDataSamples.sampleName}
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
            {editSamples && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>Samples ID: {editSamples.sampleId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`Samples/Update`, editSamples);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditSamples(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>Samples Name:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editSamples.sampleName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditSamples({ ...editSamples, sampleName: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditSamples(null)}
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

export default Samples;