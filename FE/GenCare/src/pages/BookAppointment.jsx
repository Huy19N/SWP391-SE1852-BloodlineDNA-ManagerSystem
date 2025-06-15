import React, { useState } from "react";

const daysOfWeek = ["Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7"];
const timeSlots = ["slot 1", "slot 2", "slot 3", "slot 4"];

export default function BookAppointment() {
  const [selectedSlot, setSelectedSlot] = useState({ day: "", slot: "" });

  const handleSelect = (day, slot) => {
    setSelectedSlot({ day, slot });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!selectedSlot.day || !selectedSlot.slot) {
      alert("Vui lòng chọn 1 khung giờ.");
      return;
    }
    alert(`Bạn đã chọn ${selectedSlot.slot} vào ${selectedSlot.day}`);
  };

  return (
    <div className="container mt-5 p-4 rounded shadow" style={{ maxWidth: "900px", backgroundColor: "#f9f9f9" }}>
      <h1 className="text-center">lấy của chat, chưa học</h1>
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
                      style={{ cursor: "pointer" }}
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

      {/* Nút xác nhận */}
      <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit}>
          Đăng ký
        </button>
      </div>
    </div>
  );
}