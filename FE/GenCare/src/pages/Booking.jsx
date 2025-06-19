import React, { useState, useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';


function Booking() {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
    serviceType: '',
    serviceDetail: '',
    testType: '',
    timeSlot: '',
    method: '',
    fullName: '',
    gmail: '',
    birthDate: '',
    cccd: '',
  });

  // Load selectedService từ localStorage
  useEffect(() => {
    const selectedService = JSON.parse(localStorage.getItem('selectedService'));
    if (selectedService) {
      setFormData((prev) => ({
        ...prev,
        serviceType: selectedService.mainType || '',
        serviceDetail: selectedService.subType || '',
        testType: selectedService.testType || '',
        timeSlot: `${selectedService.appointmentDay || ''} - ${selectedService.appointmentSlot || ''}`,
        method: selectedService.sampleMethod || '',
      }));
    }
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    
  };
  const handleSubmit = () => {
     //  API tại đây 
    const {gmail, birthDate, cccd, fullName } = formData;
    // điều kiện điền thông tin
  if (!gmail || !birthDate || !cccd || !fullName) {
      toast.error("Vui lòng điền đầy đủ thông tin cá nhân!");
      return;
    }
    toast.success("Đăng ký thành công!");
    console.log("Thông tin đăng ký:", formData);
    navigate('/');
};

  return (
    <div className="container mt-5 mb-4 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
    <div className="d-flex align-items-center mb-5">
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        <h2 className="mx-4 text-primary text-center">ĐĂNG KÝ XÉT NGHIỆM</h2>
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
    </div>

    {/* Loại dịch vụ */}
    <div className="mb-4">
    <label className="block font-medium mb-2">Loại dịch vụ:</label>
    <input
        type="text"
        name="serviceType"
        value={formData.serviceType}
        readOnly
        className="w-full p-2 border rounded bg-light"
    />
    </div>

    {/* Chi tiết dịch vụ */}
    <div className="mb-4">
    <label className="block font-medium mb-2">Chi tiết dịch vụ:</label>
    <input
        type="text"
        name="serviceDetail"
        value={formData.serviceDetail}
        readOnly
        className="w-full p-2 border rounded bg-light"
    />
    </div>

    {/* Loại xét nghiệm */}
    <div className="mb-4">
    <label className="block font-medium mb-2">Loại xét nghiệm:</label>
    <input
        type="text"
        name="testType"
        value={formData.testType}
        readOnly
        className="w-full p-2 border rounded bg-light"
    />
    </div>

    {/* Khung giờ */}
    <div className="mb-4">
    <label className="block font-medium mb-2">Khung giờ:</label>
    <input
        type="text"
        name="timeSlot"
        value={formData.timeSlot}
        readOnly
        className="w-full p-2 border rounded bg-light"
    />
    </div>

    {/* Phương thức thu mẫu */}
        <div className="mb-4">
    <label className="block font-medium mb-2">Phương thức thu mẫu:</label>
    <input
        type="text"
        name="method"
        value={formData.method}
        readOnly
        className="w-full p-2 border rounded bg-light"
    />
    </div>

    {/* Họ tên */}
    <div className="mb-4">
        <label className="block font-medium mb-2">Họ và tên:</label>
        <input
        type="text"
        name="fullName"
        value={formData.fullName}
        onChange={handleChange}
        className="w-full p-2 border rounded"
        placeholder="Nhập họ và tên"
        />
    </div> 

    {/* Gmail */}
        <div className="mb-4">
        <label className="block font-medium mb-2">Gmail:</label>
        <input
        type="email"
        name="gmail"
        value={formData.gmail}
        onChange={handleChange}
        className="w-full p-2 border rounded"
        placeholder="Nhập gmail"
        />
    </div>

    {/* CCCD */}
    <div className="mb-4">
        <label className="block font-medium mb-2">CCCD:</label>
        <input
        type="text"
        name="cccd"
        value={formData.cccd}
        onChange={handleChange}
        className="w-full p-2 border rounded"
        placeholder="Nhập số CCCD"
        />
    </div>

    {/* Năm sinh */}
    <div className="mb-4">
        <label className="block font-medium mb-2">Năm sinh :</label>
        <input
        type="date"
        name="birthDate"
        value={formData.birthDate}
        onChange={handleChange}
        className="w-full p-2 border rounded"
        />
    </div>
    
    {/* Nút gửi */}
    <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit} >
          Gửi
        </button>
      </div>
    </div>
);
}

export default Booking;