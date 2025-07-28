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
            console.log("Lỗi DataBlog", error.dataBlog);
            console.log("Lỗi Data User", error.dataUser);
            toast.error("Thất bại khi tải dữ liệu Blog!");
        }
        finally{
            setIsLoading(false);
        }
    }

    //API gọi delete Blogs
    const fetchDelete = async (blogId) => {
        if(!window.confirm("Bạn có đồng ý blog này không?")) return;

        try{
            await api.delete(`Blogs/DeleteById/${blogId}`);
            toast.success("Blog này đã xóa thành công!");

            fetchData();
        }
        catch (error){
            console.log("Xóa Lỗi", error);
            toast.error("Thất Bại khi xóa blog này!")
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
            toast.success("Blog này đã tạo thành công!");
            setShowCreateModal(false);
            setFromDataBlog({ userId: '', title: '', content: '', createdAt: '' });
            fetchData();
        } catch (error) {
            console.error("Lỗi tạo blog", error);
            toast.error("Thất bại tạo blog này");
        }
    };

    const getUsername = (userId) => {
        const user = dataUser.find(u => u.userId === userId);
        return user ? user.fullName : 'trống';
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
                Danh Sách Bài Đăng
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
                        Thêm Bài Đăng Mới
                    </button>
                </div>
            </div>
            

            <table className="table table-bordered table-hover shadow">
                <thead className="table-primary text-center">
                <tr>
                    <th>Mã</th>
                    <th>Tên NGười Dùng</th>
                    <th>Tựa Đề</th>
                    <th>Ngày</th>
                    {(isAdmin || isManager) ? <th>Hành Động</th> : null}
                </tr>
                </thead>
                <tbody>
                {isLoading ? (
                    <tr>
                    <td colSpan="5" className="text-center">Tải...</td>
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
                    <td colSpan="5" className="text-center">Không có blog nào cả.</td>
                    </tr>
                )}
                </tbody>
            </table>
            {/* Thêm blog*/}
            {showCreateModal && (
                <div className="update-overlay">
                    <div className="update-box">
                        <h4 className="mb-3">Tạo Blog Mới</h4>
                        <form onSubmit={handleCreateSubmit}>
                            <div className="mb-3">
                                <label className="form-label">Mã Người Dùng: </label>
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
                                <label className="form-label">Tựa Đề: </label>
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
                                <label className="form-label">Nội Dung: </label>
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
                                <label className="form-label">Ngày: </label>
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
                                <button type="button" className="btn btn-secondary me-2" onClick={() => setShowCreateModal(false)}>Hủy</button>
                                <button type="submit" className="btn btn-primary">Tạo</button>
                            </div>
                        </form>
                    </div>
                </div>
            )}


            {/*Update here nha */}
            {editBlog && (
            <div className="update-overlay">
            <div className="update-box">
                <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
                <h5>Mẫ: {editBlog.blogId}</h5>
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
                    <label>Mã Người Dùng: </label>
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
                    <label>Tựa Đề: </label>
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
                    <label>Nội Dung: </label>
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
                    <label>Ngày: </label>
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
                    Lưu
                </button>
                <button
                    className="btn btn-secondary"
                    type="button"
                    onClick={() => setEditBlog(null)}
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

export default Blog;