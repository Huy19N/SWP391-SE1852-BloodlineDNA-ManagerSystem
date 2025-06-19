import React from "react";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children, allowedRoles}) => {
    // Lấy roleId từ localStorage
    const role = JSON.parse(localStorage.getItem('roleId'));
    // Nếu không có roleId, người dùng chưa đăng nhập
    // Nếu có roleId, kiểm tra xem roleId có nằm trong allowedRoles không
    // Nếu không có roleId, chuyển hướng đến trang đăng nhập
    if(!role){
        return <Navigate to="/login" replace />;
    }
    // Nếu có roleId, kiểm tra xem roleId có nằm trong allowedRoles không
    // Nếu không có, chuyển hướng về trang chủ
    if(!allowedRoles.includes(role)){
        return <Navigate to="/" replace />;
    }
    // Nếu roleId hợp lệ, hiển thị children
    return children;
};

export default ProtectedRoute;