import React, { useState } from "react";
import { NavLink, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import "../css/index.css";

export default function Header() {
  const navigate = useNavigate();
  const [theme, setTheme] = useState("light");

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userId");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("roleId");
    navigate("/login");
    toast.success("Đăng xuất thành công!");
  };

  const handleLogin = () => {
    const token = localStorage.getItem("token");
    const refresh = localStorage.getItem("refreshToken");

    if (token && refresh) {
      const roleId = localStorage.getItem("roleId");
      const isAdmin = roleId === "4";
      const isStaff = roleId === "2";
      const isManager = roleId === "3";

      if (isAdmin || isStaff || isManager) {
        return (
          <li className="nav-item dropdown">
            <a
              className="nav-link dropdown-toggle"
              href="#!"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Quản Lý
            </a>
            <ul className="dropdown-menu">
              <li>
                <NavLink className="dropdown-item" to="/layout">
                  Thống Kê
                </NavLink>
              </li>
              <li>
                <NavLink className="dropdown-item" to="/account">
                  Thông Tin Tài Khoản
                </NavLink>
              </li>
              <li>
                <button className="dropdown-item" onClick={handleLogout}>
                  Đăng Xuất
                </button>
              </li>
            </ul>
          </li>
        );
      }

      if (roleId === "1") {
        return (
          <li className="nav-item dropdown">
            <a
              className="nav-link dropdown-toggle"
              href="#!"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Tài Khoản
            </a>
            <ul className="dropdown-menu">
              <li>
                <NavLink className="dropdown-item" to="/account">
                  Thông Tin Tài Khoản
                </NavLink>
              </li>
              <li>
                <NavLink className="dropdown-item" to="/mybooking">
                  Lịch Sử Đăng Ký Xét Nghiệm
                </NavLink>
              </li>
              <li>
                <button className="dropdown-item" onClick={handleLogout}>
                  Đăng Xuất
                </button>
              </li>
            </ul>
          </li>
        );
      }
    }

    return (
      <li className="nav-item">
        <NavLink className="nav-link" to="/login">
          Đăng Nhập
        </NavLink>
      </li>
    );
  };

  const toggleTheme = () => {
    if (theme === "light") {
      document.body.classList.add("bg-dark", "text-light");
      setTheme("dark");
    } else {
      document.body.classList.remove("bg-dark", "text-light");
      setTheme("light");
    }
  };

  return (
    <nav className={`navbar navbar-expand-lg fixed-top glass-navbar ${theme === "dark" ? "navbar-dark" : "navbar-light"}`}>
      <div className="container">
        <NavLink className="navbar-brand fw-bold" to="/">
          GeneCare
        </NavLink>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#mainNavbar"
          aria-controls="mainNavbar"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="mainNavbar">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <NavLink className="nav-link" to="/">Trang Chủ</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/Instruction">Hướng dẫn</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/services">Dịch Vụ</NavLink>
            </li>
            <li className="nav-item">
              <NavLink className="nav-link" to="/blog">Bài đăng</NavLink>
            </li>
            {handleLogin()}
          </ul>
          <button className="btn btn-outline-secondary ms-3" onClick={toggleTheme}>
            {theme === "light" ? <i className="bi bi-moon"></i> : <i className="bi bi-sun"></i>}
          </button>
        </div>
      </div>
    </nav>
  );
}
