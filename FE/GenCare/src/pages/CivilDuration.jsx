import React from "react";
import { useNavigate } from 'react-router-dom';

function CivilDuration() {
  const selectedService = JSON.parse(localStorage.getItem('selectedService')) || {};
  const navigate = useNavigate();

  // Danh sách gói thời gian
  const durations = [
    { label: "Gói 6h", price: "2,500,000đ" },
    { label: "Gói 24h", price: "2,000,000đ" },
    { label: "Gói 48h", price: "1,500,000đ" }
  ];


  const handleSelectDuration = (duration) => {
    const previous = JSON.parse(localStorage.getItem('selectedService')) || {};
    localStorage.setItem('selectedService', JSON.stringify({
      ...previous,
      durationType: duration
    }));
    navigate("/book-appointment");
  };

  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>Bảng giá dịch vụ dân sự</h1>
        {selectedService.testType && (
          <p className="fs-4 mt-3">
            Bạn đang chọn dịch vụ xét nghiệm: <strong>{selectedService.testType}</strong>
          </p>
        )}
      </div>

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 className="mx-4 text-primary text-center">CHỌN GÓI THỜI GIAN</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>

        <div className="row">
          {durations.map((item, index) => (
            <div key={index} className="col-md-4 mb-4">
              <div
                className="card shadow text-dark text-decoration-none"
                onClick={() => handleSelectDuration(item.label)}
                style={{ cursor: "pointer" }}
              >
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">{item.label.toUpperCase()} CÓ KẾT QUẢ</h4>
                </div>
                <div className="card-body p-0">
                  <table className="table table-hover mb-0">
                    <tbody>
                      <tr>
                        <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                          {item.price} / 1 người
                        </td>
                      </tr>
                      <tr><td></td><td>Kết quả bảo mật tuyệt đối</td></tr>
                      <tr><td></td><td>Độ chính xác &gt; 99.9999%</td></tr>
                      <tr><td></td><td>Miễn phí xét nghiệm mở rộng nếu không huyết thống</td></tr>
                      <tr><td></td><td>Thu mẫu tại nhà MIỄN PHÍ</td></tr>
                      <tr><td></td><td>Không phụ thu thêm phí mẫu đặc biệt</td></tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default CivilDuration;