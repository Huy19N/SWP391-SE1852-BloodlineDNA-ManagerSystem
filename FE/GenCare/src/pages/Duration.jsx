import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import api from "../config/axios";


function Duration() {
  const navigate = useNavigate();
  const [data, setData] = useState([]);
  const selectedService = JSON.parse(localStorage.getItem('selectedService')) || {};
  

  useEffect(() => {
    const fetchData = async () => {
      
      try {
        const response = await api.get("ServicePrices/GetAllPaging", {
          params: {
            typeSearch: "",
            search: "",
            sortBy: "PriceID",
            page: 1,
            
          },
        });

        const priceList = response.data.data ;

        const promises = priceList.map(async (item) => {
          const [serviceRes, durationRes] = await Promise.all([
            api.get(`Services/GetById/${item.serviceId}`), 
            api.get(`Durations/GetById/${item.durationId}`), 
          ]);

          return {
            ...item,
            service: serviceRes.data.data,
            duration: durationRes.data.data,
          };
        });

        const fullData = await Promise.all(promises);

        // lọc theo dịch vụ đã chọn
        const normalize = (text) =>text?.toLowerCase().normalize("NFD");// xóa dấu và in hoa
        const filtered = fullData.filter(
          (entry) =>
            normalize(entry.service.serviceName) === normalize(selectedService.mainType)
        );

        setData(filtered);
      } catch (error) {
        console.error("Lỗi khi gọi API:", error);
      }
    };
    

    fetchData();
  }, []);

  const handleSelect = (duration, price, serviceId, durationId) => {
  const prev = JSON.parse(localStorage.getItem("selectedService")) || {};
  localStorage.setItem("selectedService", JSON.stringify({
    ...prev,
    durationType: duration.durationName,
    price,
    serviceId,
    durationId
  }));
  navigate("/book-appointment");
};
  
  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>Bảng giá dịch vụ {selectedService?.mainType}</h1>
        {selectedService.testType && (
          <p className="fs-4 mt-3">
            Bạn đang chọn dịch vụ: <strong>{selectedService.testType}</strong>
          </p>
        )}
      </div>

      <div className="container mt-4 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="row">
          {data.length === 0 && (
            <div className="text-center fs-4 text-danger">Không tìm thấy gói thời gian phù hợp.</div>
          )}

          {data.map((item) => (
            <div key={item.priceId} className="col-md-4 mb-4">
              <div
                className="card shadow text-dark text-decoration-none"
                onClick={() => handleSelect(item.duration, item.price, item.service.serviceId, item.duration.durationId)}
              >
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">{item.duration.durationName.toUpperCase()} CÓ KẾT QUẢ</h4>
                </div>
                <div className="card-body p-0">
                  <table className="table table-hover mb-0">
                    <tbody>
                      <tr>
                        <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                          {item.price.toLocaleString("vi-VN")} đ / 1 người
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

export default Duration;
