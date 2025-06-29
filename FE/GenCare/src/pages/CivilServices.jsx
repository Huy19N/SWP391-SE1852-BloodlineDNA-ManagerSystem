import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function CivilServices() {
  const navigate = useNavigate();
  const [services, setServices] = useState([]);

  const selectedService = JSON.parse(localStorage.getItem("selectedService"));

  const handleSelect = (mainType, testType) => {
    localStorage.setItem(
      "selectedService",
      JSON.stringify({ mainType, testType })
    );
    navigate("/civil-duration");
  };

  // Dữ liệu mẫu tạm thời thay cho API
  useEffect(() => {
    const mockCivilData = [
      { id: 1, mainType: 'dân sự', testType: 'Cha/Mẹ-Con', imageUrl: '' },
      { id: 2, mainType: 'dân sự', testType: 'Anh/Chị-Em', imageUrl: '' },
      { id: 3, mainType: 'dân sự', testType: 'song sinh', imageUrl: '' },
      { id: 4, mainType: 'dân sự', testType: 'Cô/Chú-Cháu', imageUrl: '' },
      { id: 5, mainType: 'dân sự', testType: 'Dì/Cậu-Cháu', imageUrl: '' },
      { id: 6, mainType: 'dân sự', testType: 'Ông/Bà-Cháu', imageUrl: '' },
      { id: 7, mainType: 'dân sự', testType: 'bệnh di truyền', imageUrl: '' },
      { id: 8, mainType: 'dân sự', testType: 'gen đột biến', imageUrl: '' },
      { id: 9, mainType: 'dân sự', testType: 'sàng lọc trước sinh', imageUrl: '' },
      { id: 10, mainType: 'dân sự', testType: 'di truyền ung thư', imageUrl: '' },
      { id: 11, mainType: 'dân sự', testType: 'định danh cá nhân', imageUrl: '' },
      { id: 12, mainType: 'dân sự', testType: 'động vật', imageUrl: '' },
      { id: 13, mainType: 'dân sự', testType: 'huyết thống thai nhi', imageUrl: '' }
    ];
    setServices(mockCivilData);
  }, []);

  const filteredServices = services.filter(
    (s) =>
      s.mainType === selectedService?.mainType 
  );

  return (
    <div className="container mt-5" style={{ paddingTop: "2rem" }}>
      <div className="text-center mb-4">
        <h1>
          Dịch vụ {selectedService?.mainType} 
        </h1>
      </div>

      <div
        className="container mt-4 p-4 rounded shadow"
        style={{ background: "rgba(255, 255, 255, 0.9)" }}
      >
        <div className="row">
          {filteredServices.map((service) => (
            <div className="col-md-4 rounded-3 mb-4" key={service.id}>
              <div
                className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
                onClick={() =>
                  handleSelect(service.mainType,  service.testType)
                }>
                <img
                  src={service.imageUrl || "/Images/default.jpg"}
                  className="card-img-top"
                  alt={service.testType}
                  style={{ objectFit: "cover", height: "250px" }}
                />
                <div className="card-body">
                  <h3>{service.testType}</h3>
                </div>
              </div>
            </div>
          ))}

          {filteredServices.length === 0 && (
            <div className="text-center fs-4 text-danger">
              Không tìm thấy dịch vụ nào phù hợp với lựa chọn.
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default CivilServices;