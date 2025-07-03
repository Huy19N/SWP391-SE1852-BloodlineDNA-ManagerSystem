import React, { useState, useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import api from '../config/axios';

  const selectedService = JSON.parse(localStorage.getItem('selectedService'));
  const userId = localStorage.getItem("userId"); //lấy id
function Booking() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    serviceType: '',
    testType: '',
    timeSlot: '',
    method: '',
    serviceId: null,
    durationId: null,
    user: {
      fullName: '',
      gmail: '',
      cccd: ''
    },
    person1: {
    fullName: '',
    birthDate: '',
    gender: '',
    hasTestedDna: '',
    sampleType: '',
    relationToPerson2: ''
    },
    person2: {
    fullName: '',
    birthDate: '',
    gender: '',
    hasTestedDna: '',
    sampleType: '',
    relationToPerson1: ''
    }
  });

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const userRes = await api.get(`/Users/getbyid/${userId}`);
        const user = userRes.data.data;

        setFormData((prev) => ({
          ...prev,
          serviceType: selectedService?.mainType || '',
          testType: selectedService?.testType || '',
          timeSlot: `${selectedService?.appointmentDay || ''} - ${selectedService?.appointmentSlot || ''}`,
          method: selectedService?.sampleMethod || '',
          serviceId: selectedService?.serviceId || null,
          durationId: selectedService?.durationId || null,
          user: {
            userId: user.userId,
            fullName: user.fullName,
            gmail: user.email,
            cccd: user.identifyId
          }
        }));
      } catch (error) {
        console.error("Lỗi khi lấy thông tin người dùng:", error);
        toast.error("Không thể tải thông tin người dùng.");
      }
    };

    fetchUserData();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    if (name.startsWith('person1.')) {
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

  const handleSubmit = async (e) => {
  e.preventDefault();

  if (!formData.person1.fullName || !formData.person1.birthDate || !formData.person1.gender || !formData.person1.sampleType) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ nhất!");
    return;
  }

  if (!formData.person2.fullName || !formData.person2.birthDate || !formData.person2.gender || !formData.person2.sampleType) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ hai!");
    return;
  }

  try {
    

    const bookingData = {
      userId: parseInt(userId),
      durationId: selectedService?.durationId,
      serviceId: selectedService?.serviceId,
      methodId: selectedService?.methodId || 1,
      appointmentTime: new Date().toISOString(),
      statusId: 1,
      date: new Date().toISOString(),
      patients: [
        {
          fullName: formData.person1.fullName,
          birthDate: formData.person1.birthDate,
          gender: formData.person1.gender,
          identifyId: String(formData.user.cccd),
          sampleType: formData.person1.sampleType,
          hasTestedDna: formData.person1.hasTestedDna=== 'true',//xét true khi chọn có
          relationship: formData.person1.relationToPerson2
        },
        {
          fullName: formData.person2.fullName,
          birthDate: formData.person2.birthDate,
          gender: formData.person2.gender,
          identifyId: String(formData.user.cccd),
          sampleType: formData.person2.sampleType,
          hasTestedDna: formData.person2.hasTestedDna=== 'true',
          relationship: formData.person2.relationToPerson1
        }
      ]
    };
    console.log("Dữ liệu gửi đi:", bookingData);
    console.log("Patients chi tiết:", bookingData.patients);
    const res = await api.post("Patient/CreatePatientWithBooking", bookingData);

    toast.success("Đăng ký thành công!");
    navigate("/payment");

  } catch (error) {
    console.error("Lỗi khi gửi đăng ký:", error);
    toast.error("Đã xảy ra lỗi, vui lòng thử lại.");
  }
};



  return (
    <div className="container mt-5 mb-4 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
      <ToastContainer />
      <div className="d-flex align-items-center mb-5">
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        <h2 className="mx-4 text-primary text-center">ĐĂNG KÝ XÉT NGHIỆM</h2>
        <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
      </div>

      {['serviceType', 'testType', 'timeSlot', 'method'].map((field) => (
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

      <Section title="Thông tin người đăng ký">
        <TextInput label="Họ và tên" name="user.fullName" value={formData.user.fullName} readOnly />
        <TextInput label="Gmail" name="user.gmail" value={formData.user.gmail} readOnly type="email" />
        <TextInput label="CCCD" name="user.cccd" value={formData.user.cccd} readOnly />
      </Section>

      <Section title="Thông tin người thứ nhất">
        <TextInput label="Họ và tên" name="person1.fullName" value={formData.person1.fullName} onChange={handleChange} />
        <TextInput label="Năm sinh" name="person1.birthDate" value={formData.person1.birthDate} onChange={handleChange} type="date" />
        <SelectInput label="Giới tính" name="person1.gender" value={formData.person1.gender} onChange={handleChange} options={genderOptions} />
        <SelectInput label="Đã xét nghiệm trước đây chưa?" name="person1.hasTestedDna" value={formData.person1.hasTestedDna}onChange={handleChange} options={testedOptions}/>
        <SelectInput label="Loại mẫu xét nghiệm" name="person1.sampleType" value={formData.person1.sampleType} onChange={handleChange} options={sampleOptions} />
        <TextInput label="Mối quan hệ với người thứ 2" name="person1.relationToPerson2" value={formData.person1.relationToPerson2} onChange={handleChange} />
      </Section>

      <Section title="Thông tin người thứ hai">
        <TextInput label="Họ và tên" name="person2.fullName" value={formData.person2.fullName} onChange={handleChange} />
        <TextInput label="Năm sinh" name="person2.birthDate" value={formData.person2.birthDate} onChange={handleChange} type="date" />
        <SelectInput label="Giới tính" name="person2.gender" value={formData.person2.gender} onChange={handleChange} options={genderOptions} />
        <SelectInput label="Đã xét nghiệm trước đây chưa?" name="person2.hasTestedDna" value={formData.person2.hasTestedDna} onChange={handleChange} options={testedOptions}/>
        <SelectInput label="Loại mẫu xét nghiệm" name="person2.sampleType" value={formData.person2.sampleType} onChange={handleChange} options={sampleOptions} />
        <TextInput label="Mối quan hệ với người thứ 1" name="person2.relationToPerson1" value={formData.person2.relationToPerson1} onChange={handleChange} />
      </Section>

      <div className="text-center mt-4">
        <button className="btn btn-primary px-4" onClick={handleSubmit}>
          Đăng ký
        </button>
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

const TextInput = ({ label, name, value, onChange, type = "text", readOnly = false }) => (
  <div className="mb-4">
    <label className="block font-medium mb-2">{label}:</label>
    <input
      type={type}
      name={name}
      value={value}
      onChange={onChange}
      readOnly={readOnly}
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

const testedOptions = [
  { value: 'true', label: 'Rồi nha má' },
  { value: 'false', label: 'Chưa nè' }
];
const sampleOptions = [
  { value: 'blood', label: 'Máu' },
  { value: 'nail', label: 'Móng tay/chân' },
  { value: 'hair', label: 'Tóc' },
  { value: 'buccal-swab', label: 'Niêm mạc miệng' }
];

export default Booking;
