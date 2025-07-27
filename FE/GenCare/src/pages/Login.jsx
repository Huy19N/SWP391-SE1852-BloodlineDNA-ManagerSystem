import React, { useState, useRef, useEffect } from 'react';
import api from '../config/axios';
import { ToastContainer, toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import OTPModal from '../components/OTPmodal.jsx';
import '../css/login.css';

const LoginRegister = () => {
  const [isActive, setIsActive] = useState(false);
  const [dataLoginGoogle, setDataLoginGoogle] = useState([]);
  const [showOTPModal, setShowOTPModal] = useState(false);
  const [registerEmail, setRegisterEmail] = useState("");
  const containerRef = useRef(null);
  const navigate = useNavigate();
  // Loading state for form submission
  const [isLoading, setIsLoading] = useState(false);
  // Data for register form
  const [fromDataRegister, setFromDataRegister] = useState({
    email: '',
    password: '',
    confirmPassword: ''
  });
  // Data for login form
  const [fromDataLogin, setFromDataLogin] = useState({
    email: '',
    password: ''
  });
// Function to handle input changes for Login form
  const handleInputChangeLogin = (e) => {
    const { name, value } = e.target;
    setFromDataLogin({
      ...fromDataLogin,
      [name]: value
    });
  };
  // Function to handle input changes for register form
    const handleInputChangeRegister = (e) => {
    const { name, value } = e.target;
    setFromDataRegister({
      ...fromDataRegister,
      [name]: value
    });
  };
  useEffect(() => {
    // Apply or remove active class based on state
    if (containerRef.current) {
      if (isActive) {
        containerRef.current.classList.add('active');
      } else {
        containerRef.current.classList.remove('active');
      }
    }
  }, [isActive]);


  useEffect(() => {
  const params = new URLSearchParams(window.location.search);
  const accessToken = params.get("AccessToken");
  const refreshToken = params.get("RefreshToken");
  const roleId = params.get("roleId");
  const userId = params.get("userId");

  if (accessToken && refreshToken && roleId && userId) {
    // Lưu thông tin vào localStorage
    localStorage.setItem("token", accessToken);
    localStorage.setItem("refreshToken", refreshToken);
    localStorage.setItem("roleId", roleId);
    localStorage.setItem("userId", userId);

    toast.success("Đăng nhập bằng Google thành công!");
    navigate("/"); // chuyển về trang chủ
  }
}, []);

  const handleRegisterClick = () => {
    setIsActive(true);
  };

  const handleLoginClick = () => {
    setIsActive(false);
  };

  const handleLoginSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    // Call Login API here
    try{
      console.log('Login loaded: ', fromDataLogin);


      const response = await api.post('Auth/Login', fromDataLogin);
      console.log('Login response: ', response.data.data);


       // Xử lý response dựa trên status code
      if (response.status === 200) {
        // Nếu có data trả về
        if (response.data) {
            console.log('Login response data: ', response.data);
        }

        const responseData = response.data.data;
        console.log("Login success with role:", responseData.role);
        console.log("UserId :", responseData.userId);
        // Lưu token và role vào localStorage
        localStorage.setItem('token', responseData.accessToken);
        localStorage.setItem('refreshToken', responseData.refreshToken);
        localStorage.setItem('roleId', responseData.role);
        localStorage.setItem('userId',responseData.userId);
        const roleID = responseData.role;
        // Điều hướng theo vai trò
        if (roleID != null) {
          navigate('/');
          toast.success("Đăng nhập thành công!"); // Chuyển hướng về trang chủ nếu là customer
        } 





      } else if (response.status === 204) {
        // No content nhưng thành công
        toast.success("Đăng nhập thành công!");
        navigate('/'); // Chuyển hướng về trang chủ sau khi đăng nhập thành công
      }
    }
    catch (err){
      console.error('Login error: ', err);
      toast.error("Wrong Email or password.Please enter again!");
    }finally{
      setIsLoading(false);
    }

    
  };

  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    // Kiểm tra xem mật khẩu và xác nhận mật khẩu có khớp nhau không
    if (fromDataRegister.password !== fromDataRegister.confirmPassword) {
      toast.error("Mật Khẩu Không Trùng Khớp!");
      setIsLoading(false);
      return;
    }

    // Kiểm tra định dạng email
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(fromDataRegister.email)) {
      toast.error("Email không hợp lệ! Vui lòng nhập đúng định dạng (ví dụ: example@gmail.com)");
      setIsLoading(false);
      return;
    }

    // Kiểm tra định dạng mật khẩu
    const passwordRegex = /^(?=.*[0-9])(?=.*[!@#$%^&*])[\S]{6,20}$/;
    if (!passwordRegex.test(fromDataRegister.password)) {
      toast.error("Mật khẩu phải chứa ít nhất 1 số, 1 ký tự đặc biệt và không chứa khoảng trắng!");
      setIsLoading(false);
      return;
    }

    // Call Register API here
      try {
      const response = await api.post("Auth/Register", fromDataRegister);

      if (response.status === 200 || response.status === 204) {
        // Gửi verify email
        await api.post(`VerifyEmail/SendVerifyEmail`, null, {
          params: { email: fromDataRegister.email },
        });

        setRegisterEmail(fromDataRegister.email);
        setShowOTPModal(true); // Hiện modal nhập OTP
        toast.success("Vui lòng kiểm tra email để xác minh!");
      }
    } catch (err) {
      console.error('Register error: ', err);
      toast.error("Tài khoản đã tồn tại!");
    } finally {
      setIsLoading(false);
    }
  };


  const handleGoogleLogin = async (e) => {
    e.preventDefault();
    setIsLoading(true);
    try {
      const response = await api.get('Auth/GoogleLogin');
      console.log('Google Login response: ', response.data.data);
      
      if (response.status === 200) {
        // Nếu có data trả về
        if (response.data.data) {
          console.log('Google Login response data: ', response.data.data);
          // setDataLoginGoogle(response.data.data);
          // localStorage.setItem('token', response.data.data.accessToken);
          // localStorage.setItem('refreshToken', response.data.data.refreshToken);
          // localStorage.setItem('roleId', response.data.data.role);
          // localStorage.setItem('userId',response.data.data.userId);
          window.location.href = response.data.data;
          // toast.success("Đăng nhập thành công!");
        }
      } else if (response.status === 204) {
        // No content nhưng thành công
        // toast.success("Đăng nhập thành công!");
        // navigate('/');
      }
    } catch (err) {
      console.error('Google Login error: ', err);
      toast.error("Đăng nhập bằng Google thất bại!");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
      <div className="auth-container" id="container" ref={containerRef}>
          <OTPModal
            show={showOTPModal}
            email={registerEmail}
            onClose={() => setShowOTPModal(false)}
            onVerified={() => {
              setShowOTPModal(false);
              toast.success("Tài khoản đã xác minh thành công!");
              navigate('/login');
            }}
          />
        {/* Login Form */}
        <div className="form-box login">
          <form onSubmit={handleLoginSubmit}>
            <h1 className="mb-4">Đăng nhập</h1>
            
            <div className="input-box mb-4">
              <input 
              id='email'
              name="email"
              value={fromDataLogin.email}
              onChange={handleInputChangeLogin}
              type="text" 
              className="form-control custom-input" 
              placeholder="Email" 
              required
              
              />
              <i className="bi bi-envelope-fill input-icon"></i>
            </div>

            <div className="input-box mb-3">
              <input 
              id='password'
              name="password"
              value={fromDataLogin.password}
              onChange={handleInputChangeLogin}
              type="password" 
              className="form-control custom-input" 
              placeholder="Mật khẩu" 
              required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <div className="forgot-link mb-3">
              <a href="#" className="text-decoration-none">Bạn quên mật khẩu ?</a>
            </div>

            <button 
            id='submit-btn'
            type="submit" 
            className="btn custom-btn w-100 mb-3"
            disabled={isLoading}
            >
              {isLoading ? "Logining...." : "Đăng nhập"}
            </button>

            <p className="text-center mb-3">Hoặc đăng nhập bằng tài khoản google</p>

            <div className="social-icons d-flex justify-content-center gap-2">
              <a className="social-link" onClick={handleGoogleLogin}>
                <i className="bi bi-google"></i>
              </a>
            </div>
          </form>
        </div>

        {/* Register Form */}
        <div className="form-box register">
          <form onSubmit={handleRegisterSubmit}>
            <h1 className="mb-4">Đăng ký</h1>
            
            <div className="input-box mb-3">
              <input 
              id='emailRegister'
              name="email"
              value={fromDataRegister.email}
              onChange={handleInputChangeRegister}
              type="email" 
              className="form-control custom-input" 
              placeholder="Email" 
              required />
              <i className="bi bi-envelope-fill input-icon"></i>
            </div>

            <div className="input-box mb-3">
              <input 
              id='passwordRegister'
              name="password"
              value={fromDataRegister.password}
              onChange={handleInputChangeRegister}
              type="password" 
              className="form-control custom-input" 
              placeholder="Mật khẩu" 
              required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <div className="input-box mb-4">
              <input 
              id='confirmPasswordRegister'
              name="confirmPassword"
              value={fromDataRegister.confirmPassword}
              onChange={handleInputChangeRegister}
              type="password" 
              className="form-control custom-input" 
              placeholder="Xác nhận mật khẩu" 
              required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <button 
            type="submit" 
            className="btn custom-btn w-100 mb-3"
            disabled={isLoading}
            >
              {isLoading ? "Registering...." : "Đăng ký"}
            </button>

            <p className="text-center mb-3">Hoặc đăng ký bằng tài khoản google</p>

            <div className="social-icons d-flex justify-content-center gap-2">
              <a className="social-link" onClick={handleGoogleLogin}>
                <i className="bi bi-google"></i>
              </a>
            </div>
          </form>
        </div>

        {/* Toggle Box */}
        <div className="toggle-box">
          <div className="toggle-panel toggle-left d-flex flex-column justify-content-center align-items-center text-center">
            <h1 className="mb-3">GenCare xin chào!</h1>
            <p className="mb-4">bạn không có tài Khoản?</p>
            <button 
              className="btn toggle-btn" 
              onClick={handleRegisterClick}
            >
              Đăng ký
            </button>
          </div>
          
          <div className="toggle-panel toggle-right d-flex flex-column justify-content-center align-items-center text-center">
            <h1 className="mb-3">GenCare xin chào!</h1>
            <p className="mb-4">Bạn đã có tài khoản?</p>
            <button 
              className="btn toggle-btn" 
              onClick={handleLoginClick}
            >
              Đăng nhập
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginRegister;
