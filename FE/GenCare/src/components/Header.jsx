import { NavLink } from 'react-router-dom';
import  { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
 import {  toast } from 'react-toastify';


export default function Header(){
    const navigate = useNavigate();
    // Hàm đăng xuất
    const handleLogout = () => {
        // Xóa token và roleId khỏi localStorage
        localStorage.removeItem('token');
        localStorage.removeItem('userId');
        localStorage.removeItem('refreshToken');
        localStorage.removeItem('roleId');
        // Chuyển hướng về trang đăng nhập hoặc trang chủ
        navigate('/login')
        toast.success("Đăng xuất thành công!")
    };

    // Hàm này sẽ thay đổi login khi người dùng đã đăng nhập, nếu đã đăng nhập thì sẽ hiện account và nó sẽ dropdown menu xuống hiện các như logout và information và lịch sử xét nghiệm còn
    // nếu là admin thì dropdown menu sẽ hiện management và logout
    const handleLogin = () => {
        // Kiểm tra xem người dùng đã đăng nhập hay chưa
        const isLoggedIn = localStorage.getItem('token'); //lấy token đăng nhập trong localStorage
        const isLoggedInRefresh = localStorage.getItem('refreshToken'); //lấy refreshToken đăng nhập trong localStorage
        if (isLoggedIn && isLoggedInRefresh) {
            const roleId = localStorage.getItem('roleId'); //lấy roleId đăng nhập trong localStorage
            // Kiểm tra xem người dùng có phải là admin hay không
            const isAdmin = roleId === '4';
            const isStaff = roleId === '2';
            const isManager = roleId === '3';
            if (isAdmin || isStaff || isManager) {
                // Nếu là admin, hiển thị menu quản lý
                return (
                    <li className="nav-item dropdown">
                        <a className="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Quản Lý
                        </a>
                        <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                            <li><NavLink className="dropdown-item" to="/layout">Thống Kê</NavLink></li>
                            <li><NavLink className="dropdown-item" to="/account">Thông Tin Tài Khoản</NavLink></li>
                            <li><hr className="dropdown-divider" /></li>
                            {localStorage.getItem('token') ? (
                                <li className="nav-item">
                                    <button className="btn btn-link nav-link text-dark" onClick={handleLogout}>Đăng Xuất</button>
                                </li>
                            ) : (
                                <NavLink className="nav-link text-dark" to="/login">Đăng Nhập</NavLink>
                            )}
                        </ul>
                    </li>
                );
            }
            // Nếu là người dùng bình thường, hiển thị menu tài khoản
            else if (roleId === '1') {
                return (
                <li className="nav-item dropdown">
                    <a className="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Tài Khoản
                    </a>
                    <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li><NavLink className="dropdown-item" to="/account">Thông Tin Tài Khoản</NavLink></li>
                        <li><NavLink className="dropdown-item" to="/mybooking">Lịch Sử Đăng Ký Xét Nghiệm</NavLink></li>
                        <li><hr className="dropdown-divider" /></li>
                        {localStorage.getItem('token') ? (
                                <li className="nav-item">
                                    <button className="btn btn-link nav-link text-dark" onClick={handleLogout}>Đăng Xuất</button>
                                </li>
                            ) : (
                                <NavLink className="nav-link text-dark" to="/login">Đăng Nhập</NavLink>
                            )}
                    </ul>
                </li>
                );
            }
        } else {
            // Nếu chưa đăng nhập, hiển thị nút đăng nhập
            return (
                <li className="nav-item">
                    <NavLink className="nav-link text-dark" to="/login">Đăng Nhập</NavLink>
                </li>
            );
        }
    };

    // Hàm đăng xuất
    return (
        <header className='fixed-top '>
        <nav className="navbar navbar-expand-sm navbar-light bg-white    border-bottom box-shadow "style={{ height: '56px'}} >
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">GeneCare</NavLink>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/">Trang Chủ</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/Instruction">Hướng dẫn</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/">Về Chúng Tôi</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/services">Dịch Vụ</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/blog">Blog</NavLink>
                        </li>
						<li className="nav-item">
                            {handleLogin()}
                        </li>   
                    </ul>
                    
                </div>
            </div>
        </nav>
    </header>

    );
}
