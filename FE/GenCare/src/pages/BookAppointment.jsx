import React, { useState } from "react";
import { ToastContainer, toast, } from 'react-toastify';
import { useNavigate } from "react-router-dom";

const daysOfWeek = ["Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"];
const timeSlots = ["slot 1", "slot 2", "slot 3", "slot 4"];

function BookAppointment() {
  const navigate = useNavigate();
  const selectedService = JSON.parse(localStorage.getItem('selectedService'));

  const [selectedSlot, setSelectedSlot] = useState({ day: "", slot: "" });
  const [selectedMethod, setSelectedMethod] = useState("");

  const handleSelect = (day, slot) => {
    setSelectedSlot({ day, slot });
  };

  const handleMethodChange = (event) => {
    setSelectedMethod(event.target.value);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!selectedSlot.day || !selectedSlot.slot) {
      toast("Vui lòng chọn 1 khung giờ.");
      return;
    }
    if (!selectedMethod) {
      toast.warn("Vui lòng chọn phương thức thu mẫu.");
      return;
    }
    const updatedService = {
      ...selectedService,
      appointmentDay: selectedSlot.day,
      appointmentSlot: selectedSlot.slot,
      sampleMethod: selectedMethod
    };

    localStorage.setItem('selectedService', JSON.stringify(updatedService));

    toast(`Bạn đã chọn ${selectedSlot.slot} vào ${selectedSlot.day} và ${selectedMethod}`);
    navigate('/booking');
  };

  return (
    <div className="container mt-5 p-4 mb-4 rounded shadow" style={{ maxWidth: "900px", backgroundColor: "#f9f9f9" }}>
      {selectedService && (
          <p className="fs-4 text-center">
            Bạn đã chọn <strong>{selectedService.mainType}</strong> - <strong>{selectedService.subType}</strong> <br></br>
            Loại xét nghiệm <strong>{selectedService.testType}</strong> và gói <strong>{selectedService.durationType}</strong>
          </p>
        )}
      <h2 className="text-center text-primary mb-4">Đặt lịch hẹn</h2>

      {/* Bảng chọn thời gian */}
      <div className="table-responsive">
        <table className="table table-bordered text-center align-middle">
          <thead className="table-light">
            <tr>
              <th></th>
              {daysOfWeek.map((day) => (
                <th key={day}>{day}</th>
              ))}
            </tr>
          </thead>
          <tbody>
            {timeSlots.map((slot) => (
              <tr key={slot}>
                <th>{slot}</th>
                {daysOfWeek.map((day) => {
                  const isSelected = selectedSlot.day === day && selectedSlot.slot === slot;
                  return (
                    <td
                      key={day + slot}
                      className={`selectable-slot ${isSelected ? "bg-success text-white" : ""}`}
                      onClick={() => handleSelect(day, slot)}
                    >
                      {isSelected ? "Đã chọn" : "Chọn"}
                    </td>
                  );
                })}
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <h2 className="text-center text-primary mb-4">Chọn phương thức lấy mẫu</h2>

      <div className="mb-4">
        <label className="form-label">Phương thức thu mẫu:</label>
        <select value={selectedMethod} onChange={handleMethodChange} className="form-select">
          <option value="">-- Chọn --</option>
          <option value="Tự thu mẫu tại nhà">Tự thu mẫu tại nhà</option>
          <option value="Thu mẫu tại nhà">Thu mẫu tại nhà</option>          
          <option value="Thu mẫu tại cơ sở y tế">Thu mẫu tại cơ sở y tế</option>
        </select>
      </div>
      {/* Nút xác nhận */}
      <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit}>
          Đăng ký
        </button>
      </div>
    </div>
  );
}
export default BookAppointment;