import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../config/axios";

function CollectionMethod() {
  const [methods, setMethods] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchMethods = async () => {
      try {
        const response = await api.get("CollectionMethod/GetAll");
        setMethods(response.data.data);
      } catch (error) {
        console.error("Lỗi khi lấy phương thức thu mẫu:", error);
      }
    };

    fetchMethods();
  }, []);

  const handleSelect = (method) => {
    const prev = JSON.parse(localStorage.getItem("selectedService")) || {};
    localStorage.setItem("selectedService", JSON.stringify({
      ...prev,
      collectionMethod: method.methodName,
      collectionMethodId: method.methodId,
    }));
    navigate("/duration"); // Hoặc route kế tiếp tuỳ bạn
  };

  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>Chọn phương thức thu mẫu</h1>
        <p className="fs-5 text-muted mt-2">Vui lòng chọn một trong các phương thức thu mẫu bên dưới</p>
      </div>

      <div
        className="container mt-4 p-4 rounded shadow"
        style={{ background: 'rgba(255, 255, 255, 0.9)' }}
      >
        <div className="row">
          {methods.length === 0 && (
            <div className="text-center fs-4 text-danger">Không tìm thấy phương thức nào.</div>
          )}

          {methods.map((method) => (
            <div key={method.methodId} className="col-md-4 mb-4">
              <div
                className="card shadow text-center p-4 h-100 hoverable"
                onClick={() => handleSelect(method)}
                style={{ cursor: "pointer", background: "#f0f8ff" }}
              >
                <div className="card-body d-flex align-items-center justify-content-center">
                  <h4 className="mb-0 text-primary">{method.methodName}</h4>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default CollectionMethod;
