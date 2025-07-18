import { useState, useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import api from '../config/axios';
import { parse, format } from 'date-fns';

const userId = localStorage.getItem("userId");

function Booking() {
  const navigate = useNavigate();


  const [selectedService, setSelectedService] = useState({});
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
      cccd: '',
      address: '',
      phone: ''
    },
    person1: {
      fullName: '',
      birthDate: '',
      gender: '',
      hasTestedDna: '',
      sampleID: '',
      relationToPerson2: ''
    },
    person2: {
      fullName: '',
      birthDate: '',
      gender: '',
      hasTestedDna: '',
      sampleID: '',
      relationToPerson1: ''
    }
  });

  // Lấy selectedService từ localStorage
  useEffect(() => {
    const data = JSON.parse(localStorage.getItem("selectedService")) || {};
    setSelectedService(data);
  }, []);
  useEffect(() => {
    const data = JSON.parse(localStorage.getItem("selectedService")) || {};
    console.log("selectedService trong localStorage:", data);
    setSelectedService(data);
  }, []);
  // Lấy thông tin user và cập nhật form theo selectedService
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
          method: selectedService?.collectionMethod || '',
          serviceId: selectedService?.serviceId || null,
          durationId: selectedService?.durationId || null,
          user: {
            userId: user.userId,
            fullName: user.fullName,
            gmail: user.email,
            cccd: user.identifyId,
            address: user.address,
            phone: user.phone
          }
        }));
      } catch (error) {
        console.error("Lỗi khi lấy thông tin người dùng:", error);
        toast.error("Không thể tải thông tin người dùng.");
      }
    };

    if (selectedService?.mainType) {
      fetchUserData();
    }
  }, [selectedService]);

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

  const handleSubmit = async (e) => {
  e.preventDefault();
  if (!formData.user.fullName || !formData.user.cccd || !formData.user.address || !formData.user.phone) {
  toast.error("Vui lòng điền đầy đủ thông tin người đăng ký!");
  return;
  }

  if (!formData.person1.fullName || !formData.person1.birthDate || !formData.person1.gender || !formData.person1.sampleID) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ nhất!");
    return;
  }

  if (!formData.person2.fullName || !formData.person2.birthDate || !formData.person2.gender || !formData.person2.sampleID) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ hai!");
    return;
  }

  try {
    // cập nhật user
    const userRes = await api.get(`/Users/getbyid/${userId}`);
    const existingUser = userRes.data.data;

    const updatedUser = {
      ...existingUser, // giữ lại password, roleId, lastPwdChange, ...
      fullName: formData.user.fullName,
      email: formData.user.gmail,
      identifyId: formData.user.cccd,
      address: formData.user.address,
      phone: formData.user.phone,
      lastPwdChange: new Date().toISOString(), 
    };
    await api.put(`Users/Update/${userId}`, updatedUser);

    const getAppointmentTime = (dayStr, slotStr) => {
    const timeMap = {
    'slot 1': '08:00',
    'slot 2': '10:00',
    'slot 3': '13:00',
    'slot 4': '15:00',
    };

  const time = timeMap[slotStr] || '08:00';
  const parsedDate = parse(dayStr, 'dd/MM/yyyy', new Date());

  //ghép ngày và giờ lại thành chuỗi 
  const dateTimeString = format(parsedDate, 'yyyy-MM-dd') + 'T' + time + ':00';
  return new Date(dateTimeString).toISOString();
  };
    
    //tạo bôking
    const bookingData = {
      userId: parseInt(userId),
      durationId: selectedService?.duration?.durationId || selectedService?.durationId,
      serviceId: selectedService?.serviceId,
      methodId: selectedService?.collectionMethodId,
      appointmentTime: getAppointmentTime(selectedService?.appointmentDay, selectedService?.appointmentSlot),
      date: new Date().toISOString(),
      statusId: 1,
      patients: [
        {
          patientId: 0,
          bookingId: 0,
          fullName: formData.person1.fullName,
          birthDate: formData.person1.birthDate,
          gender: formData.person1.gender,
          identifyId: String(formData.user.cccd),
          sampleId: parseInt(formData.person1.sampleID),
          hasTestedDna: formData.person1.hasTestedDna === 'true',
          relationship: formData.person1.relationToPerson2
        },
        {
          patientId: 0,
          bookingId: 0,
          fullName: formData.person2.fullName,
          birthDate: formData.person2.birthDate,
          gender: formData.person2.gender,
          identifyId: String(formData.user.cccd),
          sampleId: parseInt(formData.person2.sampleID),
          hasTestedDna: formData.person2.hasTestedDna === 'true',
          relationship: formData.person2.relationToPerson1
        }
      ]
    };

    console.log("Dữ liệu gửi đi:", bookingData);
    if (!formData.user.gmail) {
    toast.error("Không thể cập nhật vì thiếu email. Vui lòng cập nhật Gmail trong thông tin người dùng.");
    return;
    }
    const response = await api.post("Patient/CreatePatientWithBooking", bookingData);
    if (response.data?.data?.bookingId) {
    localStorage.setItem("bookingId", response.data.data.bookingId);
  }
    //  Lấy bookingId từ response 
    const bookingId = response?.data?.data?.bookingId;
    const methodId = selectedService?.collectionMethodId;

    if (!bookingId) {
      toast.error("Không thể lấy bookingId để tạo testprocess.");
      return;
    }

    //  lấy bước dựa theo colectionmethod
    const getStepsByCollectionMethod = (methodId) => {
      const fullSteps = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
      const shortSteps = [1, 2, 3, 7, 8, 9, 10];
      return methodId === 1 ? fullSteps : shortSteps;
    };

    const steps = getStepsByCollectionMethod(methodId);
    const stepCount = steps.length.toString();//tính số bước để cho vào description
    //  Gửi lần lượt các testprocess
    for (let i = 0; i < steps.length; i++) {
      const step = steps[i];

      let statusId;
      if (i < 3) {
        statusId = 4; 
      } else if (i === 3) {
        statusId = 3;
      } else {
        statusId = 2; 
      }

      const process = {
        bookingId: bookingId,
        stepId: step,
        statusId: statusId,
        description: stepCount + " bước",
        updatedAt: new Date().toISOString()
      };

      try {
        await api.post("/TestProcess/Create", process);
        console.log(`Đã tạo bước ${step} với statusId ${statusId}`);
      } catch (err) {
        console.error(`Lỗi khi tạo bước ${step}:`, err);
      }
    }

    toast.success("Đăng ký thành công!");
    navigate("/payment", {
      state: {
        bookingId: response?.data?.data?.bookingId,
        price: selectedService?.price,
        user: {
          fullName: formData.user.fullName,
          email: formData.user.gmail
        },
        appointmentTime: `${selectedService?.appointmentDay} - ${selectedService?.appointmentSlot}`
      }
    });

  } catch (error) {
    console.error("Lỗi khi gửi đăng ký:", error);
    console.log("Chi tiết:", error.response?.data);
    toast.error("Đã xảy ra lỗi, vui lòng thử lại.");
  }

  
};

  return (
  <div className="container mt-5 mb-4 p-4 rounded shadow bg-white">
    <ToastContainer />
    <h2 className="text-center text-primary border-bottom pb-2 mb-4">ĐĂNG KÝ XÉT NGHIỆM</h2>

    <form onSubmit={handleSubmit}>
      {/* Thông tin dịch vụ */}
      {['serviceType', 'testType', 'timeSlot', 'method'].map((field) => (
        <div className="mb-3" key={field}>
          <label className="form-label text-capitalize">{field}</label>
          <input
            type="text"
            name={field}
            value={formData[field]}
            readOnly
            className="form-control bg-light"
          />
        </div>
      ))}

      {/* Người đăng ký */}
      <h4 className="text-primary border-bottom pb-2 mt-4">Thông tin người đăng ký</h4>
      <div className="mb-3">
        <label className="form-label">Họ và tên</label>
        <input className="form-control" name="user.fullName" value={formData.user.fullName} onChange={handleChange}/>
      </div>
      <div className="mb-3">
        <label className="form-label">Gmail</label>
        <input className="form-control bg-light" name="user.gmail" value={formData.user.gmail} type="email" onChange={handleChange} readOnly/>
      </div>
      <div className="mb-3">
        <label className="form-label">CCCD</label>
        <input className="form-control" name="user.cccd" value={formData.user.cccd} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <label className="form-label">Địa chỉ</label>
        <input className="form-control" name="user.address" value={formData.user.address} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <label className="form-label">Số điện thoại</label>
        <input className="form-control" name="user.phone" value={formData.user.phone} onChange={handleChange} />
      </div>
      
      {/* Người thứ nhất */}
      <h4 className="text-primary border-bottom pb-2 mt-4">Thông tin người thứ nhất</h4>
      <div className="mb-3">
        <label className="form-label">Họ và tên</label>
        <input placeholder="Họ và tên" name="person1.fullName" value={formData.person1.fullName} onChange={handleChange} className="form-control" />
      </div>
      <div className="mb-3">
        <label className="form-label">Năm sinh</label>
        <input type="date" name="person1.birthDate" value={formData.person1.birthDate} onChange={handleChange} className="form-control" />
      </div>
      <div className="mb-3">
        <label className="form-label">Giới tính</label>
        <select name="person1.gender" value={formData.person1.gender} onChange={handleChange} className="form-select">
          {genderOptions.map(opt => <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Đã xét nghiệm trước đây chưa?</label>
        <select name="person1.hasTestedDna" value={formData.person1.hasTestedDna} onChange={handleChange} className="form-select">
          {testedOptions.map(opt => <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Loại mẫu xét nghiệm</label>
        <select name="person1.sampleID" value={formData.person1.sampleID} onChange={handleChange} className="form-select">
          {sampleOptions.map(opt => 
          <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Mối quan hệ với người thứ 2</label>
        <input placeholder="Ghi rõ mối quan hệ" name="person1.relationToPerson2" value={formData.person1.relationToPerson2} onChange={handleChange} className="form-control" />
      </div>

      {/* Người thứ hai */}
      <h4 className="text-primary border-bottom pb-2 mt-4">Thông tin người thứ hai</h4>
      <div className="mb-3">
        <label className="form-label">Họ và tên</label>
        <input placeholder="Họ và tên" name="person2.fullName" value={formData.person2.fullName} onChange={handleChange} className="form-control" />
      </div>
      <div className="mb-3">
        <label className="form-label">Năm sinh</label>
        <input type="date" name="person2.birthDate" value={formData.person2.birthDate} onChange={handleChange} className="form-control" />
      </div>
      <div className="mb-3">
        <label className="form-label">Giới tính</label>
        <select name="person2.gender" value={formData.person2.gender} onChange={handleChange} className="form-select">
          {genderOptions.map(opt =>
             <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Đã xét nghiệm trước đây chưa?</label>
        <select name="person2.hasTestedDna" value={formData.person2.hasTestedDna} onChange={handleChange} className="form-select">
          {testedOptions.map(opt => <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Loại mẫu xét nghiệm</label>
        <select name="person2.sampleID" value={formData.person2.sampleID} onChange={handleChange} className="form-select">
          {sampleOptions.map(opt => <option key={opt.value} value={opt.value}>{opt.label}</option>)}
        </select>
      </div>
      <div className="mb-3">
        <label className="form-label">Mối quan hệ với người thứ 1</label>
        <input placeholder="Ghi rõ mối quan hệ" name="person2.relationToPerson1" value={formData.person2.relationToPerson1} onChange={handleChange} className="form-control" />
      </div>

      {/* Nút submit */}
      <div className="text-center mt-4">
        <button type="submit" className="btn btn-primary px-4">
          Đăng ký
        </button>
      </div>
    </form>
  </div>
);

}

const genderOptions = [
  { value: '', label: '-- Hãy chọn --' },
  { value: 'male', label: 'Nam' },
  { value: 'female', label: 'Nữ' },
  { value: 'other', label: 'Khác' }
];

const testedOptions = [
  { value: '', label: '-- Hãy chọn --' },
  { value: 'true', label: 'Đã từng xét nghiệm trước đây' },
  { value: 'false', label: 'Chưa từng xét nghiệm trước đây' }
];

const sampleOptions = [
  { value: '', label: '-- Hãy chọn --' },
  { value: '1', label: 'Máu' },
  { value: '2', label: 'Móng tay/chân' },
  { value: '3', label: 'Tóc' },
  { value: '4', label: 'Niêm mạc miệng' }
];

export default Booking;
