import React, {useEffect} from "react";
import api from "../../../../config/axios.js";
import { useState } from 'react';
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { Button } from "bootstrap";
import '../../../../css/index.css';


function Users(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUsers, setDataUsers] = useState([]);
    const [dataRoles, setDataRoles] = useState([]);
    const [search,setSearch] = useState('');
    const [editUser,setEditUser] = useState(null);
    const navigate = useNavigate();

    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';
    
    //API gọi dữ liệu của user và role 
    const fetchDataUser = async (e) => {
    setIsLoading(true);

        try{
            const [userRes,roleRes] = await Promise.all([
                api.get('Users/GetAll'),
                api.get('Roles/GetAll'),
            ]);
            
            const userData = userRes.data.data;
            const roleData = roleRes.data.data;

            //đây là ánh xạ từ roleId từ table user tới roleid từ table role để lấy rolename 
            const roleMap = {};
            roleData.forEach((role) => {
                roleMap[role.roleId] = role.roleName;
            });
            console.log(userData);
            console.log(roleData);
            setDataUsers(userData);
            setDataRoles(roleMap);

        }
        catch(error){
            console.error("Tải Lỗi:",error);
            toast.error(error.response.data);
        }
        finally{
            setIsLoading(false);
        }
    }

    const handleLogout = () => {
        // Xóa token và roleId khỏi localStorage
        localStorage.removeItem('token');
        localStorage.removeItem('roleId');
        // Chuyển hướng về trang đăng nhập hoặc trang chủ
        navigate('/login')
        toast.success("Đăng xuất do bạn đã không còn tồn tại!")
    };

    //API gọi delete user
    const fetchDelete = async (userId) => {
        if(!window.confirm("Bạn đồng ý muốn xóa người dùng này chứ?")) return;

        try{
            await api.delete(`Users/DeleteById/${userId}`);
            toast.success("Người dùng xóa thành công!");

            const currentUserId = localStorage.getItem('userId');
            if(parseInt(currentUserId) === userId){
                handleLogout();
            }
            else{
                fetchDataUser();//Load lại dữ liệu 
            }
        }
        catch (error){
            console.log("Xóa Lỗi", error);
            toast.error("Thất Bại xóa Người dùng này!")
        }
    }


    useEffect(() => {
            fetchDataUser();
        },[]);
    

    //Filter 
    const filteredUsers = dataUsers.filter((user) => {
        const keyword = search.toLowerCase();
        return (
            user.userId.toString().includes(keyword) ||
            user.email.toLowerCase().includes(keyword) ||
            dataRoles[user.roleId].toLowerCase().includes(keyword)
        );
    });

    return(
        <div className="container mt-5">
        <div className="h2 pb-2 mb-4 text-primary border-bottom border-primary ">
            Người dùng
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
        </div>

        <table className="table table-bordered table-hover">
            <thead className="table-primary text-center">
            <tr>
                <th>Mã</th>
                <th>Email</th>
                <th>Vai trò</th>
                {isAdmin ? <th>Hành động</th> : null}
            </tr>
            </thead>
            <tbody>
            {isLoading ? (
                <tr>
                <td colSpan="4" className="text-center">Tải...</td>
                </tr>
            ) : filteredUsers.length > 0 ? (
                filteredUsers.map((user) => (
                <tr key={user.userId}>
                    <td>{user.userId}</td>
                    <td>{user.email}</td>
                    <td>{dataRoles[user.roleId]}</td>
                    {isAdmin && (
                    <td>
                    <button className="btn btn-info ms-3 me-3"
                            onClick={() => setEditUser(user)}>
                        <i class="bi bi-pencil-square fs-4"></i>
                        </button>{/*xem va update user*/}
                    <button className="btn btn-danger"
                            onClick={() => fetchDelete(user.userId)}>
                        <i class="bi bi-trash3-fill fs-4"></i>
                        </button>{/*xoa user*/}
                    </td>
                    )}
                </tr>
                ))
            ) : (
                <tr>
                <td colSpan="4" className="text-center">Không có Người dùng nào cả.</td>
                </tr>
            )}
            </tbody>
        </table>
        
        {/*Update here nha */}
        {editUser && (
        <div className="update-overlay">
        <div className="update-box">
            <h4 className="text-center border-bottom text-primary">Cập Nhật</h4>
            <h5>Mã Người Dùng: {editUser.userId}</h5>
            <h5>Email: {editUser.email}</h5>
            <form
            onSubmit={async (e) => {
                e.preventDefault();
                try {
                await api.put(`Users/Update/${editUser.userId}`, editUser);
                toast.success("Cập nhật thành công!");
                fetchDataUser(); // reload lại bảng
                setEditUser(null); // ẩn form
                } catch (err) {
                toast.error("Cập nhật thất bại!");
                }
            }}
            >
            <div className="mb-2">
                <label>Email:</label>
                <input
                type="email"
                className="form-control"
                value={editUser.email}
                onChange={(e) =>
                    // ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                    //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email
                    setEditUser({ ...editUser, email: e.target.value })
                }
                />
            </div>

            <div className="mb-2">
                <label>Vai Trò:</label>
                <input
                type="number"
                className="form-control"
                placeholder="1: Khách Hàng, 2: Nhân Viên, 3: Quản Lý, 4: Quản Trị Viên"
                value={editUser.roleId}
                onChange={(e) =>
                    setEditUser({ ...editUser, roleId: parseInt(e.target.value) })
                }
                />
                <h6 className="text-danger">Chú ý hãy nhập số theo như trên vào vai trò để thay đổi vai trò của tài khoản này!</h6>
            </div>

            <div className="mb-2">
                <label>Tên Dầy Đủ:</label>
                <input
                type="text"
                className="form-control"
                value={editUser.fullName}
                onChange={(e) =>
                    setEditUser({ ...editUser, fullName: e.target.value })
                }
                />
            </div>

            <div className="mb-2">
                <label>CCCD:</label>
                <input
                type="number"
                className="form-control"
                value={editUser.identifyId}
                onChange={(e) =>
                    setEditUser({ ...editUser, identifyId: parseInt(e.target.value) })
                }
                />
            </div>

            <div className="mb-2">
                <label>Vị trí:</label>
                <input
                type="text"
                className="form-control"
                value={editUser.address}
                onChange={(e) =>
                    setEditUser({ ...editUser, address: e.target.value })
                }
                />
            </div>

            <div className="mb-2">
                <label>Số ĐIện Thoại:</label>
                <input
                type="number"
                className="form-control"
                value={editUser.phone}
                onChange={(e) =>
                    setEditUser({ ...editUser, phone: e.target.value })
                }
                />
            </div>

            <div className="mb-2">
                <label>Mật Khẩu:</label>
                <input
                type="text"
                className="form-control"
                value={editUser.password}
                onChange={(e) =>
                    setEditUser({ ...editUser, password: e.target.value })
                }
                />
            </div>

            <button className="btn btn-primary me-2" type="submit">
                Lưu
            </button>
            <button
                className="btn btn-secondary"
                type="button"
                onClick={() => setEditUser(null)}
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

export default Users;
