import React, { useState } from "react";
import { toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import { format, eachDayOfInterval, addDays, startOfWeek } from "date-fns";
import { vi } from "date-fns/locale";

const timeSlots = ["slot 1", "slot 2", "slot 3", "slot 4"];

// Tạo danh sách tuần bắt đầu từ thứ 2 (hiển thị 24 tuần)
const generateWeekRanges = (count = 24) => {
  const weeks = [];
  let currentMonday = startOfWeek(new Date(), { weekStartsOn: 1 });

  for (let i = 0; i < count; i++) {
    const monday = currentMonday;
    const saturday = addDays(monday, 5);

    weeks.push({
      label: `${format(monday, "dd/MM/yyyy")} - ${format(saturday, "dd/MM/yyyy")}`,
      start: monday,
      end: saturday,
    });

    currentMonday = addDays(currentMonday, 7);
  }

  return weeks;
};

function BookAppointment() {
  const navigate = useNavigate();
  const selectedService = JSON.parse(localStorage.getItem('selectedService'));

  const [selectedSlot, setSelectedSlot] = useState({ date: "", slot: "" });
  const [selectedWeek, setSelectedWeek] = useState(null);
  const [weekOptions] = useState(generateWeekRanges());

  const handleSelect = (date, slot) => {
    setSelectedSlot({ date, slot });
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!selectedSlot.date || !selectedSlot.slot) {
      toast("Vui lòng chọn một ngày và khung giờ.");
      return;
    }

    const updatedService = {
      ...selectedService,
      appointmentDay: format(selectedSlot.date, "dd/MM/yyyy"),
      appointmentSlot: selectedSlot.slot,
    };

    localStorage.setItem('selectedService', JSON.stringify(updatedService));

    toast(`Bạn đã chọn ${selectedSlot.slot} vào ngày ${format(selectedSlot.date, "dd/MM/yyyy")}`);
    navigate('/booking');
  };

  const validDays = selectedWeek
    ? eachDayOfInterval({ start: selectedWeek.start, end: selectedWeek.end })
    : [];

  const dayLabels = validDays.map((date) => format(date, 'EEEE', { locale: vi })); 

  return (
    <div className="container mt-5 p-4 mb-4 rounded shadow" style={{ maxWidth: "900px", backgroundColor: "#f9f9f9" }}>
      {selectedService && (
        <p className="fs-4 text-center">
          Bạn đã chọn <strong>{selectedService.mainType}</strong> - <strong>{selectedService.testType}</strong> <br />
          Loại xét nghiệm <strong>{selectedService.testType}</strong> và gói <strong>{selectedService.durationId}</strong>
        </p>
      )}

      <h2 className="text-center text-primary mb-4">Đặt lịch hẹn</h2>

      {/* Dropdown chọn tuần */}
      <div className="mb-3">
        <label className="form-label">Chọn tuần:</label>
        <select
          className="form-select"
          value={selectedWeek?.label || ''}
          onChange={(e) => {
            const week = weekOptions.find(w => w.label === e.target.value);
            setSelectedWeek(week);
            setSelectedSlot({ date: "", slot: "" });
          }}
        >
          <option value="">-- Chọn một tuần --</option>
          {weekOptions.map((week) => (
            <option key={week.label} value={week.label}>{week.label}</option>
          ))}
        </select>
      </div>

      {/* Bảng chọn thời gian */}
      <div className="table-responsive">
        {selectedWeek ? (
          <div className="table-responsive mt-3">
            <table className="table table-bordered text-center align-middle">
              <thead className="table-light">
                <tr>
                  <th></th>
                  {dayLabels.map((label, idx) => (
                    <th key={idx}>{label}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                {timeSlots.map((slot) => (
                  <tr key={slot}>
                    <th>{slot}</th>
                    {validDays.map((dateObj) => {
                      const isSelected =
                        selectedSlot.date &&
                        selectedSlot.slot === slot &&
                        format(selectedSlot.date, 'yyyy-MM-dd') === format(dateObj, 'yyyy-MM-dd');

                      return (
                        <td
                          key={format(dateObj, 'yyyy-MM-dd') + slot}
                          className={`selectable-slot ${isSelected ? "bg-success text-white" : ""}`}
                          onClick={() => handleSelect(dateObj, slot)}
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
        ) : (
          <h5 className=" text-center mt-4">Vui lòng chọn một tuần để xem các khung giờ.</h5>
        )}
      </div>

      <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit}>
          Đăng ký
        </button>
      </div>
    </div>
  );
}

export default BookAppointment;
