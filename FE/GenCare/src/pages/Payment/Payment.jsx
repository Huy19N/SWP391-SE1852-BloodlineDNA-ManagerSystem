// PaymentForm.jsx
import React, { useState, useEffect } from 'react';
import api from '../../config/axios'; 
import { toast } from 'react-toastify';

function PaymentForm() {
  const selectedService = JSON.parse(localStorage.getItem('selectedService')) ;
  const userId = localStorage.getItem("userId");
  const bookingId = localStorage.getItem("bookingId");

  const [orderType, setOrderType] = useState(selectedService.mainType || "");
  const [amount, setAmount] = useState(selectedService.price || 0);
  const [orderDescription, setOrderDescription] = useState(
    `Thanh toán dịch vụ ${selectedService.testType }`
  );
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");

  useEffect(() => {
    const fetchUser = async () => {
      if (!userId) return;

      try {
        const res = await api.get(`Users/getbyid/${userId}`);
        const user = res.data.data;
        if (user?.fullName) setName(user.fullName);
        if (user?.email) setEmail(user.email);
      } catch (err) {
        console.error("Không thể lấy thông tin người dùng:", err);
      }
    };

    fetchUser();
  }, [userId]);

  const handlePayment = async (e) => {
    e.preventDefault();

    // Kiểm tra dữ liệu đầu vào
    if (!bookingId || !email || amount <= 0) {
      toast.error("Thiếu thông tin thanh toán!");
      return;
    }

    try {
      const response = await api.post('/Payment', {
        paymentMethodId: 0,
        orderType,
        amount,
        bookingId: Number(bookingId),
        email
      });

      const result = response.data;
      if (result.success) {
        toast.success("thành công");
        window.location.href = result.data;
      } else {
        toast.error("lỗi xác nhận");
        console.log(" phản hồi API:", result);
      }
    } catch (err) {
      console.error("lỗi xác nhận:", err);
      toast.error("lỗi");
    }
  };

  return (
    <div className="container mt-5 p-5">
      <h2>VNPay Payment Form</h2>
      <form onSubmit={handlePayment}>
        <div className="mb-3">
          <label className="form-label">Order Type</label>
          <input type="text" className="form-control" value={orderType} onChange={e => setOrderType(e.target.value)} />
        </div>

        <div className="mb-3">
          <label className="form-label">Amount (VND)</label>
          <input type="number" className="form-control" value={amount} onChange={e => setAmount(Number(e.target.value))} />
        </div>

        <div className="mb-3">
          <label className="form-label">Order Description</label>
          <textarea className="form-control" value={orderDescription} onChange={e => setOrderDescription(e.target.value)}></textarea>
        </div>

        <div className="mb-3">
          <label className="form-label">Customer Name</label>
          <input type="text" className="form-control" value={name} onChange={e => setName(e.target.value)} />
        </div>

        <button type="submit" className="btn btn-primary">Pay with VNPay</button>
      </form>
    </div>
  );
}

export default PaymentForm;
