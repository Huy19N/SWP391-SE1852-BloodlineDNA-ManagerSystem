import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

function LegalServices() {
  const navigate = useNavigate();
  const [services, setServices] = useState([]);

  const selectedService = JSON.parse(localStorage.getItem("selectedService"));

  const handleSelect = (mainType, subType, testType) => {
    localStorage.setItem(
      "selectedService",
      JSON.stringify({ mainType, subType, testType })
    );
    navigate("/legal-duration");
  };

  useEffect(() => {
    const mockLegalData = [
      { id: 1, mainType: 'pháp lý', subType: 'loại 1', testType: 'Cha/Mẹ-Con', imageUrl: '' },
      { id: 2, mainType: 'pháp lý', subType: 'loại 1', testType: 'Anh/Chị-Em', imageUrl: '' },
      { id: 3, mainType: 'pháp lý', subType: 'loại 1', testType: 'họ hàng-Cháu', imageUrl: '' },
      { id: 4, mainType: 'pháp lý', subType: 'loại 1', testType: 'Ông/Bà-Cháu', imageUrl: '' },
      { id: 5, mainType: 'pháp lý', subType: 'loại 2', testType: 'hình sự', imageUrl: '' },
      { id: 6, mainType: 'pháp lý', subType: 'loại 2', testType: 'truy vết tội phạm', imageUrl: '' },
      { id: 7, mainType: 'pháp lý', subType: 'loại 2', testType: 'kiểm chứng tại tòa', imageUrl: '' },
    ];
    setServices(mockLegalData);
  }, []);

  const filteredServices = services.filter(
    (s) =>
      s.mainType === selectedService?.mainType &&
      s.subType === selectedService?.subType
  );

  return (
    <div className="container mt-5" style={{ paddingTop: "2rem" }}>
      <div className="text-center mb-4">
        <h1>
          Dịch vụ {selectedService?.mainType} - {selectedService?.subType}
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
                  handleSelect(service.mainType, service.subType, service.testType)
                }
              >
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

export default LegalServices;