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

        const priceList = response.data.data;

        const promises = priceList.map(async (item) => {
          try {
            const [serviceRes, durationRes] = await Promise.all([
              api.get(`Services/GetById/${item.serviceId}`),
              api.get(`Durations/GetById/${item.durationId}`),
            ]);

            return {
              ...item,
              service: serviceRes.data.data,
              duration: durationRes.data.data,
            };
          } catch (e) {
            console.error("Lỗi khi gọi service/duration với item:", item, e);
            return null;
          }
        });

        const fullData = (await Promise.all(promises)).filter(item => item !== null);

        const normalize = (text) => text?.toLowerCase().normalize("NFD");

        const filtered = fullData.filter(
          (entry) =>
            normalize(entry.service.serviceName) === normalize(selectedService.mainType) &&
            normalize(entry.service.serviceType) === normalize(selectedService.testType)
        );

        setData(filtered);
      } catch (error) {
        console.error("Lỗi khi gọi API ServicePrices:", error);
      }
    };

    fetchData();
  }, []);

  const handleSelect = (serviceId, durationId,durationName,priceId, price) => {
    const prev = JSON.parse(localStorage.getItem("selectedService")) || {};

    const updated = {
      ...prev,
      serviceId,
      durationId,
      durationName,
      priceId,
      price,
    };

    localStorage.setItem("selectedService", JSON.stringify(updated));
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
                onClick={() => handleSelect(item.service.serviceId, item.duration.durationId,item.duration.durationName,item.priceId, item.price)}
              >
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">{item.duration.durationName.toUpperCase()} CÓ KẾT QUẢ</h4>
                </div>
                <div className="card-body p-0">
                  <table className="table table-hover mb-0">
                    <tbody>
                      <tr>
                        <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                          {item.price.toLocaleString("vi-VN")} đ / 1 lần
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
