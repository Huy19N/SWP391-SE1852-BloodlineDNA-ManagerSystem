import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../config/axios";

function LegalServices() {
  const [services, setServices] = useState([]);
  const selectedService = JSON.parse(localStorage.getItem("selectedService"));
  const navigate = useNavigate();

 const handleSelect = (testType) => {
  const previous = JSON.parse(localStorage.getItem("selectedService")) || {};
  localStorage.setItem(
    "selectedService",
    JSON.stringify({ ...previous, testType })
  );
  navigate("/duration");
  };


  useEffect(() => {
  const fetchServices = async () => {
    try {
      const response = await api.get("Services/GetAllPaging", {
        params: {
          typeSearch: "",
          search: "",
          sortBy: "ServiceID",
          page: 1
        },
      });

      const result = response.data.data ;

      const normalize = (text) =>
        text?.toLowerCase().normalize("NFD");

      const filtered = result.filter(
        (service) =>
          normalize(service.serviceName) === normalize(selectedService.mainType)
      );

      setServices(filtered);
    } catch (error) {
      console.error("Lỗi nè:", error);
    }
  };

  fetchServices();
}, []);

  return (
    <div className="container mt-5" style={{ paddingTop: "2rem" }}>
      <div className="text-center mb-4">
        <h1>Dịch vụ {selectedService?.mainType}</h1>
      </div>

      <div
        className="container mt-4 p-4 rounded shadow"
        style={{ background: "rgba(255, 255, 255, 0.9)" }}
      >
        <div className="row">
          {services.map((service) => (
            <div className="col-md-4 rounded-3 mb-4" key={service.serviceId}>
              <div
                className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
                onClick={() =>
                  handleSelect( service.serviceType)
                }
              >
                <div className="card-body text-center py-5">
                  <h3>{service.serviceType}</h3>
                </div>
              </div>
            </div>
          ))}

          {services.length === 0 && (
            <div className="text-center fs-4 text-danger">
              Không tìm thấy dịch vụ nào.
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default LegalServices;
