import React, {useEffect} from "react";
import api from "../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';

function Blog(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataBlog, setDataBlog] = useState([]);
    const [dataUser, setDataUser] = useState([]);
    const [search,setSearch] = useState('');
    const [editBlog,setEditBlog] = useState(null);
    const [showCreateModal, setShowCreateModal] = useState(false);
    const [fromDataBlog, setFromDataBlog] = useState({
        userId: '',
        title: '',
        content: '',
        createdAt: ''
    });


    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const [resBlog, resUser] = await Promise.all([
                api.get('Blogs/GetAllPaging'),
                api.get('Users/GetAll')
            ]);
            const dataBlog = resBlog.data.data;
            const dataUser = resUser.data.data;

            console.log("dataBlog", dataBlog);
            console.log("dataUser", dataUser);

            setDataBlog(dataBlog);
            setDataUser(dataUser);

        }
        catch(error){
            console.log("Error dataBlog", error.dataBlog);
            console.log("Error Data User", error.dataUser);
            toast.error("Failed Loading Data Blogs");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete Blogs
    const fetchDelete = async (blogId) => {
        if(!window.confirm("Are you sure you want to delete this Blogs?")) return;

        try{
            await api.delete(`Blogs/DeleteById/${blogId}`);
            toast.success("This Blogs deleted successfully!");

            fetchData();
        }
        catch (error){
            console.log("Delete error", error);
            toast.error("Failed delete Blogs this!")
        }
    }

    useEffect (() => {
        fetchData();
    }, []);

    const handleCreateChange = (e) => {
    const { name, value } = e.target;
    setFromDataBlog(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateSubmit = async (e) => {
    e.preventDefault();

        try {
            await api.post('Blogs/Create', fromDataBlog);
            toast.success("Blogs created successfully!");
            setShowCreateModal(false);
            setFromDataBlog({ userId: '', title: '', content: '', createdAt: '' });
            fetchData();
        } catch (error) {
            console.error("Error creating Blogs", error);
            toast.error("Failed to create Blogs");
        }
    };

    const getUsername = (userId) => {
        const user = dataUser.find(u => u.userId === userId);
        return user ? user.fullName : 'Empty';
    };

    //Filter 
    const filteredBlogs = dataBlog.filter((blog) => {
        const keyword = search.toLowerCase();
        return (
            blog.blogId.toString().includes(keyword) ||
            getUsername(blog.userId).toLowerCase().includes(keyword) || 
            blog.title.toLowerCase().includes(keyword) ||
            blog.createdAt.toString().includes(keyword)
        );
    });



    return (
        <div className="container mt-5">
            <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
                Blog List
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
                        Add New Blog
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>ID</th>
                    <th>User Name</th>
                    <th>Title</th>
                    <th>Date</th>
                    {(isAdmin || isManager) ? <th>Action</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="5" className="text-center">Loading...</td>
                    </tr>
                ) : filteredBlogs.length > 0 ? (
                    filteredBlogs.map((blog) => (
                    <tr key={blog.blogId}>
                        <td>{blog.blogId}</td>
                        <td>{getUsername(blog.userId)}</td>
                        <td>{blog.title}</td>
                        <td>{blog.createdAt?.split("T")[0]}</td>
                        {(isAdmin || isManager) && (
                        <td>
                        <button className="btn btn-info ms-3 me-3"
                                onClick={() => setEditBlog(blog)}>
                            <i class="bi bi-pencil-square fs-4"></i>
                            </button>{/*xem va update blog */}
                        <button className="btn btn-danger"
                                onClick={() => fetchDelete(blog.blogId)}>
                            <i class="bi bi-trash3-fill fs-4"></i>
                            </button>{/*xoa blog*/}
                        </td>
                        )}
                    </tr>
                    ))
                ) : (
                    <tr>
                    <td colSpan="5" className="text-center">No blog found.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm blog*/}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Create New blog</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">UserID: </label>
                                <input
                                    type="number"
                                    name="userId"
                                    className="form-control"
                                    value={fromDataBlog.userId}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Title: </label>
                                <input
                                    type="text"
                                    name="title"
                                    className="form-control"
                                    value={fromDataBlog.title}
                                    onChange={handleCreateChange}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Content: </label>
                                <textarea
                                    name="content"
                                    className="form-control"
                                    value={fromDataBlog.content}
                                    onChange={handleCreateChange}
                                    rows={8}
                                    required
                                />
                            </div>
                            <div className="mb-3">
                                <label className="form-label">Date: </label>
                                <input
                                    type="date"
                                    name="createdAt"
                                    className="form-control"
                                    value={fromDataBlog.createdAt}
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
            {editBlog && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Update</h4>
                <h5>BlogID: {editBlog.blogId}</h5>
                <form
                onSubmit={async (e) => {
                    e.preventDefault();
                    try {
                    await api.put(`Blogs/Update`, editBlog);
                    toast.success("Cập nhật thành công!");
                    fetchData(); // reload lại bảng
                    setEditBlog(null); // ẩn form
                    } catch (err) {
                    toast.error("Cập nhật thất bại!");
                    }
                }}
                >
                <div className="mb-2">
                    <label>userID: </label>
                    <input
                    type="number"
                    className="form-control"
                    value={editBlog.userId}
                    onChange={(e) =>
                        // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                        //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                        setEditBlog({ ...editBlog, userId: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Title: </label>
                    <input
                    type="text"
                    className="form-control"
                    value={editBlog.title}
                    onChange={(e) =>
                        setEditBlog({ ...editBlog, title: e.target.value })
                    }
                    />
                </div>

                <div className="mb-2">
                    <label>Content: </label>
                    <textarea
                    name="content"
                    className="form-control"
                    value={editBlog.content}
                    onChange={(e) =>
                        setEditBlog({ ...editBlog, content: e.target.value })
                    }
                    rows={8}
                    />
                </div>

                <div className="mb-2">
                    <label>Date: </label>
                    <input
                    type="date"
                    className="form-control"
                    value={editBlog.createdAt}
                    onChange={(e) =>
                        setEditBlog({ ...editBlog, createdAt: e.target.value })
                    }
                    />
                </div>

                <button className="btn btn-primary me-2" type="submit">
                    Save
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditBlog(null)}
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

export default Blog;