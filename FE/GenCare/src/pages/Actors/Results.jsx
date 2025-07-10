import React, {useEffect} from "react";
import api from "../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Results(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataResults, setDataResults] = useState([]);
    const [search,setSearch] = useState('');
    const [editResults,setEditResults] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataResults, setFromDataResults] = useState({
        date: '',
        resultSummary: ''
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resResults = await api.get(`TestResults/GetAllPaging`);
            const dataResults = resResults.data;

            console.log("dataResults", dataResults);

            setDataResults(dataResults);

        }
        catch(error){
            console.log("Error Data Results", error.dataResults);
            toast.error("Failed Loading Data Results");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete Results
    const fetchDelete = async (resultId) => {
        if(!window.confirm("Are you sure you want to delete this Result?")) return;

        try{
            await api.delete(`TestResults/DeleteById/${resultId}`);
            toast.success("This user deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Result this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataResults(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            //Chuyên đổi giá trị từ input thành ISO string để gửi cho API 
            const formattedDate = new Date(fromDataResults.date).toISOString();

            const Load = {
                date: formattedDate,
                resultSummary: fromDataResults.resultSummary
            }

            await api.post('TestResults/Create', Load);
            toast.success("Result created successfully!");
            setShowCreateModal(false);
            setFromDataResults({ date: '', resultSummary: '' });
            fetchData();
        } catch (error) {
            console.error("Error creating Result", error);
            toast.error("Failed to create Result");
        }
    };


    //Filter 
    const filteredResults = dataResults.filter((results) => {
        const keyword = search.toLowerCase();
        return (
            results.resultId.toString().includes(keyword) ||
            results.date.toLowerCase().includes(keyword) ||
            results.resultSummary.toLowerCase().includes(keyword)
        );
    });


    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Results List
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
                    <button className="btn btn-primary me-3" onClick={() => setShowCreateModal(true)}> 
                        Add New Result
                    </button>
                </div>

            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Date</th>
                    <th>Result</th>
                    {(isAdmin || isManager || isStaff) ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="4" className="text-center">Loading...</td>
                    </tr>
                ) : filteredResults.length > 0 ? (
                    filteredResults.map((result) => (
                    <tr key={result.resultId}>
                        <td>{result.resultId}</td>
                        <td>{result.date.split("T")[0]}</td>
                        <td>{result.resultSummary}</td>
                        {(isAdmin || isManager || isStaff) && (
                        <td>
                            <button className="btn btn-info ms-3 me-3"
                                    onClick={() => setEditResults(result)}>
                                    <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update Result*/}
                            <button className="btn btn-danger"
                                    onClick={() => fetchDelete(result.resultId)}>
                                    <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa Result*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="4" className="text-center">No Results found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm Results*/}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New Result</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">date: </label>
                                <input
                                    type="date"
                                    name="date"
                                    className="form-control"
                                    value={fromDataResults.date}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Result: </label>
                                <textarea
                                    name="resultSummary"
                                    className="form-control"
                                    value={fromDataResults.resultSummary}
                                    onChange={handleCreateChange}
                                    rows={8}
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


                {/*Update results nha */}
                {editResults && (
                <div className="update-overlay">
                <div className="update-box">
                    <h4 className="text-center border-bottom text-primary">Update</h4>
                    <h5>Result ID: {editResults.resultId}</h5>
                    <form
                    onSubmit={async (e) => {
                        e.preventDefault();
                        try {
                        await api.put(`TestResults/Update`, editResults);
                        toast.success("Cập nhật thành công!");
                        fetchData(); // reload lại bảng
                        setEditResults(null); // ẩn form
                        } catch (err) {
                        toast.error("Cập nhật thất bại!");
                        }
                    }}
                    >
                    <div className="mb-2">
                        <label>date: </label>
                        <input
                        type="date"
                        className="form-control"
                        value={editResults.date}
                        onChange={(e) =>
                            // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                            //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                            setEditResults({ ...editResults, date: e.target.value })
                        }
                        />
                    </div>

                    <div className="mb-2">
                        <label>Result : </label>
                        <textarea
                        name="resultSummary"
                        className="form-control"
                        value={editResults.resultSummary}
                        onChange={(e) =>
                            setEditResults({ ...editResults, resultSummary: e.target.value })
                        }
                        rows={8}
                        />
                    </div>

                    <button className="btn btn-primary me-2" type="submit">
                        Save
                    </button>
                    <button
                        className="btn btn-danger"
                        type="button"
                        onClick={() => setEditResults(null)}
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

export default Results;