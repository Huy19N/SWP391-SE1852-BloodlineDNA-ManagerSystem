import React, { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import axios from 'axios';

export default function PaymentSuccess() {
  const [searchParams] = useSearchParams();
  const [status, setStatus] = useState('Đang xử lý...');

  useEffect(() => {
    const fetchStatus = async () => {
      try {
        const res = await axios.get('https://localhost:7722/api/Payment/VNPayResponse', {
          params: Object.fromEntries([...searchParams]),
        });

        setStatus(
          res.data.transactionStatus === '00'
            ? ' Thanh toán thành công!'
            : ' Thanh toán thất bại'
        );
      } catch (err) {
        setStatus(' Không xác minh được trạng thái thanh toán');
        console.error(err);
      }
    };

    fetchStatus();
  }, [searchParams]);

  return (
    <div className="container text-center mt-5">
      <h2>Kết quả thanh toán</h2>
      <p>{status}</p>
    </div>
  );
}
