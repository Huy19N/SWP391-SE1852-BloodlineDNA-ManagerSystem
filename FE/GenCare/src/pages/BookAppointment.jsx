import React, { useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { format, eachDayOfInterval, addDays, startOfWeek, isBefore, startOfDay } from "date-fns";
import { vi } from "date-fns/locale";


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
  const selectedService = JSON.parse(localStorage.getItem("selectedService"));

  const [selectedDate, setSelectedDate] = useState("");
  const [selectedWeek, setSelectedWeek] = useState(null);
  const [weekOptions] = useState(generateWeekRanges());

  const today = startOfDay(new Date());

  const handleSelect = (date) => {
    setSelectedDate(date);
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!selectedDate) {
      toast("Vui lòng chọn một ngày.");
      return;
    }

    const updatedService = {
      ...selectedService,
      appointmentDay: format(selectedDate, "dd/MM/yyyy"),
    };

    localStorage.setItem("selectedService", JSON.stringify(updatedService));

    toast(`Bạn đã chọn ngày ${format(selectedDate, "dd/MM/yyyy")}`);
    navigate("/booking");
  };

  const validDays = selectedWeek
    ? eachDayOfInterval({ start: selectedWeek.start, end: selectedWeek.end })
    : [];

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
          value={selectedWeek?.label || ""}
          onChange={(e) => {
            const week = weekOptions.find((w) => w.label === e.target.value);
            setSelectedWeek(week);
            setSelectedDate("");
          }}
        >
          <option value="">-- Chọn một tuần --</option>
          {weekOptions.map((week) => (
            <option key={week.label} value={week.label}>
              {week.label}
            </option>
          ))}
        </select>
      </div>

      {/* Bảng chọn ngày */}
      <div className="table-responsive">
        {selectedWeek ? (
          <div className="table-responsive mt-3">
            <table className="table table-bordered text-center align-middle">
              <thead className="table-light">
                <tr>
                  {validDays.map((dateObj, idx) => (
                    <th key={idx}>{format(dateObj, "EEEE", { locale: vi })}</th>
                  ))}
                </tr>
              </thead>
              <tbody>
                <tr>
                  {validDays.map((dateObj) => {
                    const isPast = isBefore(dateObj, today);
                    const isSelected =
                      selectedDate && format(selectedDate, "yyyy-MM-dd") === format(dateObj, "yyyy-MM-dd");

                    return (
                      <td
                        key={format(dateObj, "yyyy-MM-dd")}
                        className={`selectable-slot 
                          ${isSelected ? "bg-success text-white" : ""} 
                          ${isPast ? "bg-light text-muted" : ""}`}
                        onClick={() => !isPast && handleSelect(dateObj)}
                        style={{ cursor: isPast ? "not-allowed" : "pointer" }}
                      >
                        {isPast ? "Hết hạn" : isSelected ? "Đã chọn" : "Chọn"}
                      </td>
                    );
                  })}
                </tr>
              </tbody>
            </table>
          </div>
        ) : (
          <h5 className="text-center mt-4">Vui lòng chọn một tuần để xem các ngày.</h5>
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
