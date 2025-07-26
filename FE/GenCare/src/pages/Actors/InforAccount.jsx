import React, { useEffect, useState } from "react";
import api from '../../config/axios.js';
import '../../css/login.css';
import img1 from '../../assets/blood-drop-svgrepo-com.svg';
import { toast } from 'react-toastify';
function Account(){
    const [isLoading, setIsLoading] = useState(false);
    const [dataUser, setDataUser] = useState([]);
    const [dataRole, setDataRole] = useState([]);
    const [editUser,setEditUser] = useState(null);
    
    const idUser = localStorage.getItem('userId');
    const idRole = localStorage.getItem('roleId');

    const fetchUser = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get(`Users/getbyid/${idUser}`);
            const resRole = await api.get(`Roles/GetById/${idRole}`);
            const role = resRole.data.data;
            const user = response.data.data;
            setDataUser(user);
            setDataRole(role);

            console.log(user);
        }
        catch(error){
            console.log(error.response.data);
            toast.error("Error data user");
        }
        finally{
            setIsLoading(false);
        }
    } 

    useEffect (() => {
        fetchUser();
    },[]);


    return(
        <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
            <div className="authinfor-container d-flex" id="container">
                {/* Left side */}
                <div className="d-flex flex-column justify-content-center align-items-center bg-primary text-white p-4" style={{ width: '35%' }}>
                    <img className="img-fluid img-thumbnail bg-primary border border-0 rounded-circle w-50" src={img1} />
                    <div className="h2 m-4 p-4">
                        Tài Khoản Của Bạn
                    </div>
                </div>
                {/* Right side */}
                <div style={{ width: '65%' }}>
                    <div className="p-5" style={{ backgroundColor: 'white' }}>
                        <form>
                            <div className="row">
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Mã</label>
                                    <input type="text"
                                           className="form-control"
                                           value={dataUser?.userId || ''}
                                           readOnly/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Vai Trò</label>
                                    <input type="text" 
                                           className="form-control"
                                            value={dataRole?.roleName || ''}
                                            readOnly/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Tên Đầy Đủ</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.fullName || ''}
                                           readOnly/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">CMND/CCCD</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.identifyId?.toString() || ''}
                                           readOnly/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Vị trí</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.address || ''}
                                           readOnly/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Email</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.email || ''}
                                           readOnly/>
                                </div>

                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Số Điện Thoại</label>
                                    <input type="text" 
                                           className="form-control"
                                           value={dataUser?.phone?.toString() || ''} 
                                           readOnly/>
                                </div>
                                <div className="col-md-6 mb-3">
                                    <label className="form-label">Mật Khẩu</label>
                                    <input type="password" 
                                           className="form-control"
                                           value={dataUser?.password || ''}
                                           readOnly/>
                                </div>
                            </div>

                            <div className="d-flex justify-content-end">
                            <button className="btn btn-primary mt-3 px-4"
                                    type="button"
                                    onClick={() => setEditUser(dataUser)}>
                                    Chỉnh Sửa
                            </button>
                            </div>
                        </form>
                    </div>
                </div>
                {/*Update here nha */}
                {editUser && (
                <div className="update-overlay">
                <div className="update-box">
                    <h4 className="text-center pb-4 border-bottom border-primary text-primary">Chỉnh Sửa</h4>
                    <h5>Mã:    <span className="text-primary">{editUser.userId}</span></h5>
                    <h5>EMAIL:   <span className="text-primary">{editUser.email}</span></h5>
                    <form
                    onSubmit={async (e) => {
                        e.preventDefault();
                        try {
                        await api.put(`Users/Update/${editUser.userId}`, editUser);
                        toast.success("Cập nhật thành công!");
                        fetchUser(); // reload lại bảng
                        setEditUser(null); // ẩn form
                        } catch (err) {
                        toast.error("Cập nhật thất bại!");
                        }
                    }}
                    >
                        {/*// ... là trải mảng/array thành các phần tử ví dụ là editUser có spread toàn bộ dữ liệu cũ (userId, roleId, phone,...)
                            //khi khai báo trong setEditUser thì nó sẽ giữ lại các dữ liệu (userId, roleId,...) và thay đổi dữ liệu email */}
                    <div className="mb-2">
                        <label>Email:</label>
                        <input
                        type="email"
                        className="form-control"
                        value={editUser.email}
                        readOnly
                        />
                    </div>

                    <div className="mb-2">
                        <label>Vai Trò:</label>
                        <input
                        type="text"
                        className="form-control"
                        value={dataRole.roleName}
                        readOnly
                        />
                    </div>

                    <div className="mb-2">
                        <label>Tên Đầy Đủ:</label>
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
                        <label>Vị Trí:</label>
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
                        <label>Số Điện Thoại:</label>
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
        </div>
    );
}

export default Account;