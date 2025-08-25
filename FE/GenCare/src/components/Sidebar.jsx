import { Link } from "react-router-dom";
import { useState } from 'react';
import '../css/index.css';

export default function Sidebar() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const toggleDropdown = () => setIsDropdownOpen(!isDropdownOpen);

  const roleid = localStorage.getItem('roleId');
  const isStaff = roleid === '2';
  const isAdmin = roleid === '4';
  const isManager = roleid === '3';

  return (
    <div className="d-flex flex-column vh-100 sidebar-dark p-3">
      <h4 className="text-white mb-4">GENCARE</h4>

      {/* Thống kê */}
      <Link to="dashboard" className="nav-link-custom active">
        <i className="bi bi-grid"></i>
        <span>Thống Kê</span>
        <span className="badge bg-primary ms-auto">3</span>
      </Link>

      {/* Người dùng */}
      {(isAdmin || isManager || isStaff) && (
        <Link to="users" className="nav-link-custom">
          <i className="bi bi-person-fill"></i>
          <span>Người Dùng</span>
        </Link>
      )}

      {/* Lịch hẹn */}
      {(isAdmin || isManager || isStaff) && (
        <Link to="dbbooking" className="nav-link-custom">
          <i className="bi bi-box2-fill"></i>
          <span>Lịch Hẹn</span>
          <span className="badge bg-primary ms-auto">12</span>
        </Link>
      )}

      {/* Bài đăng */}
      {(isAdmin || isManager || isStaff) && (
        <Link to="blog" className="nav-link-custom">
          <i className="bi bi-journal-text"></i>
          <span>Bài Đăng</span>
        </Link>
      )}

      {/* Phản hồi */}
      {(isAdmin || isManager || isStaff) && (
        <Link to="feedback" className="nav-link-custom">
          <i className="bi bi-chat-left-text-fill"></i>
          <span>Phản Hồi</span>
          <span className="badge bg-primary ms-auto">5</span>
        </Link>
      )}

      {/* Dropdown Thêm */}
      {(isAdmin || isManager) && (
        <div>
          <div
            onClick={toggleDropdown}
            className="nav-link-custom dropdown-toggle"
            style={{ cursor: "pointer" }}
          >
            <i className="bi bi-plus-circle"></i>
            <span>Thêm</span>
          </div>
          {isDropdownOpen && (
            <div className="ms-4 mt-2">
              <Link to="services" className="nav-link-custom small">
                <i className="bi bi-box"></i> Dịch vụ
              </Link>
              <Link to="price" className="nav-link-custom small">
                <i className="bi bi-cash"></i> Giá Dịch Vụ
              </Link>
              <Link to="durations" className="nav-link-custom small">
                <i className="bi bi-clock"></i> Thời lượng
              </Link>
              <Link to="samples" className="nav-link-custom small">
                <i className="bi bi-archive"></i> Mẫu
              </Link>
            </div>
          )}
        </div>
      )}

      {/* Quay lại trang chủ */}
      <Link to="/" className="nav-link-custom mt-auto">
        <i className="bi bi-door-open-fill"></i>
        <span>Quay Lại Trang Chủ</span>
      </Link>
    </div>
  );
}
