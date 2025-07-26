import React, { useState } from "react";
import { useLocation } from "react-router-dom";
import api from "../../config/axios.js";
import axios from "axios";

function Payment() {
  const { state } = useLocation();
  const [method, setMethod] = useState(1);

  const {
    bookingId,
    user,
    appointmentTime,
    price
  } = state || {};

  const handlePay = async () => {
    if (!bookingId || !user?.email || !price) return;

    const paymentData = {
      paymentMethodId: method,
      orderType: "booking",
      amount: price,
      bookingId,
      email: user.email
    };

    try {
      // Bước 1: Gửi yêu cầu thanh toán
      const res = await api.post("Payment", paymentData);
      console.log("Kết quả thanh toán:", res.data.data);

      // Bước 2: Âm thầm GET booking
      const bookingRes = await api.get(`Bookings/GetById/${bookingId}`);
      const bookingData = bookingRes.data.data;

      // Bước 3: Cập nhật statusId thành 6
      const updatedBooking = {
        ...bookingData,
        statusId: 3
      };

      await api.put("Bookings/Update", updatedBooking);

      // Bước 4: Điều hướng đến trang thanh toán
      window.location.href = res.data.data;
    } catch (err) {
      console.error("Thanh toán lỗi:", err);
      alert("Thanh toán thất bại");
    }
  };

  if (!bookingId || !user || !price) {
    return <p className="text-center mt-5">Thiếu thông tin thanh toán.</p>;
  }

  return (
    <div className="container mt-5 p-4 border rounded " style={{ maxWidth: 600 }}>
      <h2 className="mb-4 text-center">Thanh toán dịch vụ</h2>

      <p><strong>Họ tên:</strong> {user.fullName}</p>
      <p><strong>Email:</strong> {user.email}</p>
      <p><strong>Ngày hẹn:</strong> {appointmentTime}</p>
      <p><strong>Giá dịch vụ:</strong> {price.toLocaleString("vi-VN")} đ</p>

      <div className="mb-3">
        <label>Chọn phương thức thanh toán:</label>
        <select
          className="form-select"
          value={method}
          onChange={(e) => setMethod(Number(e.target.value))}
        >
          <option value={1}>VNPay</option>
          <option value={2}>Momo</option>
        </select>
      </div>

      <div className="text-center">
        <button onClick={handlePay} className="btn btn-primary px-5">
          Thanh toán ngay
        </button>
      </div>
    </div>
  );
}

export default Payment;
