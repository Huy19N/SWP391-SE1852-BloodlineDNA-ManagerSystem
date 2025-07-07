import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function StepTest(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataStepTest, setDataStepTest] = useState([]);
    const [search,setSearch] = useState('');
    const [editStepTest,setEditStepTest] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataStepTest, setFromDataStepTest] = useState({
        stepName: '',
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const resStep = await api.get('TestStep/getAllTestSteps');
            const dataStep = resStep.data.data;

            console.log("Step", dataStep);

            setDataStepTest(dataStep);
        }
        catch(error){
            console.log("Error dataStep", error.dataStep);
            toast.error("Failed Loading Data StepTest");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete steptest
    const fetchDelete = async (stepId) => {
        if(!window.confirm("Are you sure you want to delete this steptest?")) return;

        try{
            await api.delete(`TestStep/DeleteById/${stepId}`);
            toast.success("This TestStep deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete TestStep this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataStepTest(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('TestStep/Create', fromDataStepTest);
            toast.success("TestStep created successfully!");
            setShowCreateModal(false);
            setFromDataStepTest({ stepName: ''});
            fetchData();
        } catch (error) {
            console.error("Error creating TestStep", error);
            toast.error("Failed to create TestStep");
        }
    };



    //Filter 
    const filteredTestStep = dataStepTest.filter((step) => {
        const keyword = search.toLowerCase();
        return (
            step.stepId.toString().includes(keyword) ||
            step.stepName.toLowerCase().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                TestStep
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
                        Add New TestStep
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>Name TestStep</th>
                    {isAdmin ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="3" className="text-center">Loading...</td>
                    </tr>
                ) : filteredTestStep.length > 0 ? (
                    filteredTestStep.map((step) => (
                    <tr key={step.stepId}>
                        <td>{step.stepId}</td>
                        <td>{step.stepName}</td>
                        {isAdmin || isManager ? 
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditStepTest(step)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update TestStep*/}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(step.stepId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa TestStep*/}
                        </td>
                        : null}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="3" className="text-center">No TestStep found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm service */}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New TestStep</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">TestStep Name</label>
                                <input
                                    type="text"
                                    name="stepName"
                                    className="form-control"
                                    value={fromDataStepTest.stepName}
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
            {editStepTest && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>Service ID: {editStepTest.stepId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`TestStep/Update`, editStepTest);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditStepTest(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>StepTest Name:</label>
                    <input
                    type="text"
                    className="form-control"
                    value={editStepTest.stepName}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditStepTest({ ...editStepTest, stepName: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditStepTest(null)}
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

export default StepTest;