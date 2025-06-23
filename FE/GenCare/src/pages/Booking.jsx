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
    user: {
      fullName: '',
      gmail: '',
      birthDate: '',
      cccd: ''
    },
    person1: {
      fullName: '',
      birthDate: '',
      gender: '',
      sampleType: '',
      relationToPerson2: ''
    },
    person2: {
      fullName: '',
      birthDate: '',
      gender: '',
      sampleType: '',
      relationToPerson1: ''
    }
  });

  useEffect(() => {
    const selectedService = JSON.parse(localStorage.getItem('selectedService'));
    if (selectedService) {
      setFormData((prev) => ({
        ...prev,
        serviceType: selectedService.mainType || '',
        serviceDetail: selectedService.subType || '',
        testType: selectedService.testType || '',
        timeSlot: `${selectedService.appointmentDay || ''} - ${selectedService.appointmentSlot || ''}`,
        method: selectedService.sampleMethod || ''
      }));
    }
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name.startsWith('user.')) {
      setFormData(prev => ({
        ...prev,
        user: {
          ...prev.user,
          [name.split('.')[1]]: value
        }
      }));
    } else if (name.startsWith('person1.')) {
      setFormData(prev => ({
        ...prev,
        person1: {
          ...prev.person1,
          [name.split('.')[1]]: value
        }
      }));
    } else if (name.startsWith('person2.')) {
      setFormData(prev => ({
        ...prev,
        person2: {
          ...prev.person2,
          [name.split('.')[1]]: value
        }
      }));
    } else {
      setFormData(prev => ({ ...prev, [name]: value }));
    }
  };

  const handleSubmit = () => {
    const { user, person1, person2 } = formData;

    if (!user.gmail || !user.birthDate || !user.cccd || !user.fullName) {
      toast.error("Vui lòng điền đầy đủ thông tin cá nhân!");
      return;
    }

    if (!person1.fullName || !person1.birthDate || !person1.gender || !person1.sampleType) {
      toast.error("Vui lòng điền đầy đủ thông tin người thứ nhất!");
      return;
    }

    if (!person2.fullName || !person2.birthDate || !person2.gender || !person2.sampleType) {
      toast.error("Vui lòng điền đầy đủ thông tin người thứ hai!");
      return;
    }

    // Không gọi API, chỉ mô phỏng gửi thành công
    console.log("Thông tin đã nhập:", formData);
    toast.success("Đăng ký thành công !");
    setTimeout(() => {
    navigate('/');
    }, 2000);
  };

  return (
    <div className="container mt-5 mb-4 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
      <ToastContainer />
      <div className="d-flex align-items-center mb-5">
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        <h2 className="mx-4 text-primary text-center">ĐĂNG KÝ XÉT NGHIỆM</h2>
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
      </div>

      {['serviceType', 'serviceDetail', 'testType', 'timeSlot', 'method'].map((field) => (
        <div className="mb-4" key={field}>
          <label className="block font-medium mb-2">{field}</label>
          <input
            type="text"
            name={field}
            value={formData[field]}
            readOnly
            className="w-full p-2 border rounded bg-light"
          />
        </div>
      ))}

      {/* Thông tin người đăng ký */}
      <Section title="Thông tin người đăng ký">
        <TextInput label="Họ và tên" name="user.fullName" value={formData.user.fullName} onChange={handleChange} />
        <TextInput label="Gmail" name="user.gmail" value={formData.user.gmail} onChange={handleChange} type="email" />
        <TextInput label="CCCD" name="user.cccd" value={formData.user.cccd} onChange={handleChange} />
        <TextInput label="Năm sinh" name="user.birthDate" value={formData.user.birthDate} onChange={handleChange} type="date" />
      </Section>

      {/* Người thứ nhất */}
      <Section title="Thông tin người thứ nhất">
        <TextInput label="Họ và tên" name="person1.fullName" value={formData.person1.fullName} onChange={handleChange} />
        <TextInput label="Năm sinh" name="person1.birthDate" value={formData.person1.birthDate} onChange={handleChange} type="date" />
        <SelectInput label="Giới tính" name="person1.gender" value={formData.person1.gender} onChange={handleChange} options={genderOptions} />
        <SelectInput label="Loại mẫu xét nghiệm" name="person1.sampleType" value={formData.person1.sampleType} onChange={handleChange} options={sampleOptions} />
        <TextInput label="Mối quan hệ với người thứ 2" name="person1.relationToPerson2" value={formData.person1.relationToPerson2} onChange={handleChange} />
      </Section>

      {/* Người thứ hai */}
      <Section title="Thông tin người thứ hai">
        <TextInput label="Họ và tên" name="person2.fullName" value={formData.person2.fullName} onChange={handleChange} />
        <TextInput label="Năm sinh" name="person2.birthDate" value={formData.person2.birthDate} onChange={handleChange} type="date" />
        <SelectInput label="Giới tính" name="person2.gender" value={formData.person2.gender} onChange={handleChange} options={genderOptions} />
        <SelectInput label="Loại mẫu xét nghiệm" name="person2.sampleType" value={formData.person2.sampleType} onChange={handleChange} options={sampleOptions} />
        <TextInput label="Mối quan hệ với người thứ nhất" name="person2.relationToPerson1" value={formData.person2.relationToPerson1} onChange={handleChange} />
      </Section>

      <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit}>Gửi</button>
      </div>
    </div>
  );
}

const Section = ({ title, children }) => (
  <div>
    <div className="d-flex align-items-center mb-5">
      <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
      <h3 className="mx-4 text-primary text-center">{title}</h3>
      <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
    </div>
    {children}
  </div>
);

const TextInput = ({ label, name, value, onChange, type = "text" }) => (
  <div className="mb-4">
    <label className="block font-medium mb-2">{label}:</label>
    <input
      type={type}
      name={name}
      value={value}
      onChange={onChange}
      className="w-full p-2 border rounded"
      placeholder={`Nhập ${label.toLowerCase()}`}
    />
  </div>
);

const SelectInput = ({ label, name, value, onChange, options }) => (
  <div className="mb-4">
    <label className="block font-medium mb-2">{label}:</label>
    <select name={name} value={value} onChange={onChange} className="form-select">
      <option value="">-- Chọn {label.toLowerCase()} --</option>
      {options.map(opt => (
        <option key={opt.value} value={opt.value}>{opt.label}</option>
      ))}
    </select>
  </div>
);

const genderOptions = [
  { value: 'male', label: 'Nam' },
  { value: 'female', label: 'Nữ' },
  { value: 'other', label: 'Khác' }
];

const sampleOptions = [
  { value: 'blood', label: 'Máu' },
  { value: 'nail', label: 'Móng tay/chân' },
  { value: 'hair', label: 'Tóc' },
  { value: 'buccal-swab', label: 'Niêm mạc miệng' }
];

export default Booking;