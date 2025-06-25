// PaymentForm.jsx
import React, { useState } from 'react';
import api from '../../config/axios'; // Import axios đã cấu hình sẵn
import { toast } from 'react-toastify';

function PaymentForm() {
  const [orderType, setOrderType] = useState("LegalService");
  const [amount, setAmount] = useState(100000);
  const [orderDescription, setOrderDescription] = useState("Payment for order LegalService");
  const [name, setName] = useState("Nguyen Thanh Dat");
  

  const handlePayment = async (e) => {
    e.preventDefault();

    try {
      const response = await api.post('/Payment', {
        orderType,
        amount,
        orderDescription,
        name
      });

      const result = response.data;
      if (result.success) {
        toast.success("Redirecting to VNPay...");
        window.location.href = result.data; // Redirect đến link VNPay
      } else {
        toast.error("Failed to create payment.");
      }
    } catch (err) {
      console.error("Payment error:", err);
      toast.error("Payment request failed.");
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
