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
    appointmentDay: '',
    method: '',
    price:0,
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
          appointmentDay: selectedService?.appointmentDay || '',
          method: selectedService?.collectionMethod || '',
          durationName: selectedService?.durationName || "", 
          price: selectedService.price|| 0,

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
  const { fullName, cccd, address, phone } = formData.user;
  const { person1, person2 } = formData;

  // Kiểm tra input người đăng ký
  if (!fullName.trim() || !address.trim() || !cccd || !phone) {
    toast.error("Vui lòng điền đầy đủ thông tin người đăng ký.");
    return ;
  }
  if (/\d/.test(fullName)) {
  toast.error("Họ và tên người đăng ký không được chứa số.");
  return;
}

  const cccdRegex = /^\d{11}$/;
  if (!cccdRegex.test(String(cccd).trim())) {
    toast.error("CCCD không hợp lệ. Phải đủ 11 chữ số.");
    return ;
  }

  const phoneRegex = /^\d{9,10}$/;
  if (!phoneRegex.test(String(phone).trim())) {
    toast.error("Số điện thoại không hợp lệ. Phải từ 9 đến 10 chữ số.");
    return ;
  }

  //Kiểm tra người thứ nhất
  if (!person1.fullName || !person1.birthDate || !person1.gender || !person1.hasTestedDna || !person1.sampleID || !person1.relationToPerson2) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ nhất!");
    return ;
  }

  if (/\d/.test(person1.fullName)) {
  toast.error("Tên người thứ nhất không được chứa số.");
  return;
  }

  const birthDate1 = new Date(person1.birthDate);
  const today = new Date();
  // Kiểm tra ngày sinh hợp lệ
  if (isNaN(birthDate1.getTime())) {
    toast.error("Ngày sinh của người thứ nhất không hợp lệ.");
    return;
  }

  if (birthDate1.getFullYear() < 1890) {
    toast.error("Năm sinh của người thứ nhất không hợp lệ. Phải từ 1890 đến hiện tại.");
    return;
  }

  if (birthDate1 > today) {
    toast.error("Ngày sinh của người thứ nhất không được lớn hơn ngày hiện tại.");
    return;
  }

  if (/\d/.test(person1.relationToPerson2)) {
    toast.error("Mối quan hệ của người thứ nhất không được chứa số.");
    return ;
  }

  // Kiểm tra người thứ hai
  if (!person2.fullName || !person2.birthDate || !person2.gender || !person2.hasTestedDna || !person2.sampleID || !person2.relationToPerson1) {
    toast.error("Vui lòng điền đầy đủ thông tin người thứ hai!");
    return ;
  }
  if (/\d/.test(person2.fullName)) {
  toast.error("Tên người thứ hai không được chứa số.");
  return;
  }

  const birthDate2 = new Date(person2.birthDate);
  if (isNaN(birthDate2.getTime())) {
    toast.error("Ngày sinh của người thứ hai không hợp lệ.");
    return;
  }
  if (birthDate2.getFullYear() < 1890) {
    toast.error("Năm sinh của người thứ hai không hợp lệ. Phải từ 1890 đến hiện tại.");
    return;
  }
  if (birthDate2 > today) {
    toast.error("Ngày sinh của người thứ hai không được lớn hơn ngày hiện tại.");
    return;
  }

  if (/\d/.test(person2.relationToPerson1)) {
    toast.error("Mối quan hệ của người thứ hai không được chứa số.");
    return ;
  }

  const getAppointmentTime = (dayStr) => {
    if (!dayStr) return null;
    const parsedDate = parse(dayStr, 'dd/MM/yyyy', new Date());
    return format(parsedDate, 'yyyy-MM-dd'); 
  };

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


    
    //tạo bôking
    const bookingData = {
      userId: parseInt(userId),
      priceId: selectedService?.priceId,   
      methodId: selectedService?.collectionMethodId,
      resultId: 0, // chưa có kết quả
      appointmentTime: getAppointmentTime(selectedService?.appointmentDay),
      statusId: 1, 
       date: new Date().toISOString(),
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
      const fullSteps = [1, 2, 3, 4];
      const shortSteps = [2, 3, 4];
      return methodId === 1 ? fullSteps : shortSteps;
    };

    const steps = getStepsByCollectionMethod(methodId);
    const stepCount = steps.length.toString();//tính số bước để cho vào description
    //  Gửi lần lượt các testprocess
    for (let i = 0; i < steps.length; i++) {
      const step = steps[i];

      const statusId = (i === 0) ? 3 : 2;

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
        appointmentTime: `${selectedService?.appointmentDay}`
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
      <div className="mb-3">
        <label className="form-label">Loại dịch vụ</label>
        <input type="text" name="serviceType" value={formData.serviceType} readOnly className="form-control bg-light" />
      </div>
      <div className="mb-3">
        <label className="form-label">Loại xét nghiệm</label>
        <input type="text" name="testType" value={formData.testType} readOnly className="form-control bg-light" />
      </div>
      <div className="mb-3">
        <label className="form-label">Ngày hẹn</label>
        <input type="text" name="appointmentDay" value={formData.appointmentDay} readOnly className="form-control bg-light" />
      </div>
      <div className="mb-3">
        <label className="form-label">Phương pháp thu mẫu</label>
        <input type="text" name="method" value={formData.method} readOnly className="form-control bg-light" />
      </div>
      <div className="mb-3">
        <label className="form-label">Gói dịch vụ</label>
        <input type="text" name="durationName" value={formData.durationName} readOnly className="form-control bg-light" />
      </div>
      <div className="mb-3">
        <label className="form-label">Giá dịch vụ</label>
        <input type="text" name="price" value={formData.price.toLocaleString("vi-VN") +"đ"}
          readOnly
          className="form-control bg-light"
        />
      </div>

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
        <input className="form-control" name="user.cccd" value={formData.user.cccd} onChange={handleChange} inputMode="numeric" pattern="\d*" />
      </div>
      <div className="mb-3">
        <label className="form-label">Địa chỉ</label>
        <input className="form-control" name="user.address" value={formData.user.address} onChange={handleChange} />
      </div>
      <div className="mb-3">
        <label className="form-label">Số điện thoại</label>
        <input className="form-control" name="user.phone" value={formData.user.phone} onChange={handleChange} inputMode="numeric" pattern="\d*" />
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
