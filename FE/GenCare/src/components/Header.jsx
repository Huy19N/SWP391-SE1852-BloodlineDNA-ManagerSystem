import { NavLink } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
import { useNavigate } from "react-router-dom";
 import { ToastContainer, toast } from 'react-toastify';
//import '../css/LightMode.css';

export default function Header(){
    const [darkMode, setDarkMode] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        if (darkMode) {
            document.body.classList.add('dark-mode');
        } else {
            document.body.classList.remove('dark-mode');
        }
    }, [darkMode]);

    // Hàm đăng xuất
    const handleLogout = () => {
        // Xóa token và roleId khỏi localStorage
        localStorage.removeItem('token');
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
        if (isLoggedIn) {
            const roleId = localStorage.getItem('roleId'); //lấy roleId đăng nhập trong localStorage
            // Kiểm tra xem người dùng có phải là admin hay không
            const isAdmin = roleId === '4';
            const isStaff = roleId === '2';
            const isManager = roleId === '3';
            if (isAdmin || isStaff || isManager) {
                // Nếu là admin, hiển thị menu quản lý
                return (
                    <li className="nav-item dropdown">
                        <a className="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                            Management
                        </a>
                        <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                            <li><NavLink className="dropdown-item" to="/layout">Dashboard</NavLink></li>
                            <li><NavLink className="dropdown-item" to="/account">Information</NavLink></li>
                            <li><hr className="dropdown-divider" /></li>
                            {localStorage.getItem('token') ? (
                                <li className="nav-item">
                                    <button className="btn btn-link nav-link text-dark" onClick={handleLogout}>Logout</button>
                                </li>
                            ) : (
                                <NavLink className="nav-link text-dark" to="/login">Login</NavLink>
                            )}
                        </ul>
                    </li>
                );
            }
            // Nếu là người dùng bình thường, hiển thị menu tài khoản
            else if (roleId === '1') {
                return (
                <li className="nav-item dropdown">
                    <a className="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Account
                    </a>
                    <ul className="dropdown-menu" aria-labelledby="navbarDropdown">
                        <li><NavLink className="dropdown-item" to="/account">Information</NavLink></li>
                        <li><NavLink className="dropdown-item" to="/test-history">My Booking</NavLink></li>
                        <li><hr className="dropdown-divider" /></li>
                        {localStorage.getItem('token') ? (
                                <li className="nav-item">
                                    <button className="btn btn-link nav-link text-dark" onClick={handleLogout}>Logout</button>
                                </li>
                            ) : (
                                <NavLink className="nav-link text-dark" to="/login">Login</NavLink>
                            )}
                    </ul>
                </li>
                );
            }
        } else {
            // Nếu chưa đăng nhập, hiển thị nút đăng nhập
            return (
                <li className="nav-item">
                    <NavLink className="nav-link text-dark" to="/login">Login</NavLink>
                </li>
            );
        }
    };

    // Hàm đăng xuất
    return (
        <header className='fixed-top '>
        <nav className="navbar navbar-expand-sm navbar-light bg-white border-bottom box-shadow "style={{ height: '56px'}} >
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">GeneCare</NavLink>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/">Home</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/Instruction">Instructions</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/">About</NavLink>
                        </li>
                        {/* <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/payment">Payment</NavLink>
                        </li> */}
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/services">Services</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link text-dark" to="/">Blog</NavLink>
                        </li>
						<li className="nav-item">
                            {handleLogin()}
                        </li>   
                    </ul>
                    {/*nút chế độ sáng tối */}
                    <p>{'chưa xong không đụng giúp tao ==>>>'} </p>
                    <button className="btn btn-outline-secondary ms-2"
                            onClick={() => setDarkMode(!darkMode)}
                            >
                            {darkMode ? "☀ Light Mode" : "🌙 Dark Mode"}
                    </button>
                </div>
            </div>
        </nav>
    </header>

    );
}
