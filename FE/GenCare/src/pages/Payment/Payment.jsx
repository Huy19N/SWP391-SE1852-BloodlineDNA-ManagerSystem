import React, { useState } from 'react';
import { createPayment } from '../Payment/PaymentApi';

export default function Payment() {
  const [method, setMethod] = useState(1); // 1 = VNPay, 2 = Momo

  const handlePay = async () => {
    const data = {
      paymentMethodId: method,
      orderType: 'booking',
      amount: 10000,
      bookingId: 1,
      email: 't@mana',
    };

    try {
      const url = await createPayment(data);
      window.location.href = url;
    } catch (err) {
      console.error(err);
      alert('Thanh toán thất bại');
    }
  };

  return (
    <div className="container mt-5">
      <h3>Chọn phương thức thanh toán</h3>
      <select value={method} onChange={(e) => setMethod(Number(e.target.value))} className="form-select mb-3">
        <option value={1}>VNPay</option>
        <option value={2}>Momo</option>
      </select>
      <button className="btn btn-primary" onClick={handlePay}>
        Thanh toán
      </button>
    </div>
  );
}
