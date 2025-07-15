import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import axios from 'axios';

export default function PaymentSuccess() {
  const [searchParams] = useSearchParams();
  const [status, setStatus] = useState('Đang xác minh trạng thái thanh toán...');
  const [isSuccess, setIsSuccess] = useState(null);

  useEffect(() => {
    const paymentId = searchParams.get("paymentId");

    if (!paymentId) {
      setStatus("Không tìm thấy mã thanh toán để kiểm tra.");
      setIsSuccess(false);
      return;
    }

    const fetchPaymentStatus = async () => {
      try {
        const allPayments = await axios.get("https://localhost:7722/api/Payment/GetAll");
        const payments = allPayments.data?.data || [];
        // So sánh kiểu string để tránh lỗi kiểu dữ liệu
        const payment = payments.find(p => String(p.paymentId) === String(paymentId));

        if (!payment) {
          setStatus("Không tìm thấy thông tin thanh toán.");
          setIsSuccess(false);
          return;
        }

      } catch (err) {
        console.error("Lỗi khi kiểm tra trạng thái thanh toán:", err);
        setStatus("Lỗi khi kiểm tra trạng thái thanh toán.");
        setIsSuccess(false);
      }
    };

    fetchPaymentStatus();
  }, [searchParams]);

  return (
    <div className="container text-center mt-5">
      <div style={{
        display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', minHeight: '60vh'
      }}>
        {isSuccess === true ? (
          <>
            <div style={{ fontSize: 64, color: 'green' }}>✔️</div>
            <h2 className="text-success mt-3">Thanh toán thành công!</h2>
            <p className="mt-2" style={{ fontSize: 20 }}>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi.</p>
            <p className="mt-2" style={{ fontSize: 18 }}>Đơn hàng của bạn đã được xác nhận thanh toán.</p>
            <a href="/" className="btn btn-success mt-4">Quay về trang chủ</a>
          </>
        ) : isSuccess === false ? (
          <>
            <div style={{ fontSize: 64, color: 'red' }}>❌</div>
            <h2 className="text-danger mt-3">Thanh toán chưa thành công</h2>
            <p className="mt-2" style={{ fontSize: 20 }}>{status}</p>
            <a href="/" className="btn btn-danger mt-4">Quay về trang chủ</a>
          </>
        ) : (
          <>
            <div style={{ fontSize: 48, color: '#888' }}>⏳</div>
            <h2 className="mt-3">Đang xác minh trạng thái thanh toán...</h2>
          </>
        )}
        
      </div>
    </div>
    
  );
}
