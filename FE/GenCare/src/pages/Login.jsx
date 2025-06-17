import React, { useState, useRef, useEffect } from 'react';
import api from '../config/axios';
 import { ToastContainer, toast } from 'react-toastify';
import { useNavigate } from "react-router-dom";
import '../css/login.css';

const LoginRegister = () => {
  const [isActive, setIsActive] = useState(false);
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


      const response = await api.post('Users/Login', fromDataLogin);
      console.log('Login response: ', response.data.data);


       // Xử lý response dựa trên status code
      if (response.status === 200) {
        // Nếu có data trả về
        if (response.data) {
            console.log('Login response data: ', response.data);
        }

        const responseData = response.data.data;
        console.log("Login success with role:", responseData.role);
        // Lưu token và role vào localStorage
        localStorage.setItem('token', responseData.accessToken);
        localStorage.setItem('roleId', responseData.role);

        // Điều hướng theo vai trò
        if (responseData.role === 1) {
          navigate('/');
          toast.success("Đăng nhập thành công!"); // Chuyển hướng về trang chủ nếu là customer
        } 
        else if (responseData.role === 4) {
          navigate('/');
          toast.success("Đăng nhập thành công!"); // Chuyển hướng đến trang admin nếu là admin
        }else if (responseData.role === 2) {
          navigate('/'); //Staff
          toast.success("Đăng nhập thành công!");
        }
        else if (responseData.role === 3) {
          navigate('/'); //Manager
          toast.success("Đăng nhập thành công!");
        }





      } else if (response.status === 204) {
        // No content nhưng thành công
        toast.success("Đăng nhập thành công!");
        navigate('/'); // Chuyển hướng về trang chủ sau khi đăng nhập thành công
      }
    }
    catch (err){
      console.error('Login error: ', err);
      toast.error(err.response.data);
    }finally{
      setIsLoading(false);
    }

    
  };

  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    setIsLoading(true);

    // Validate password and confirmPassword match
    if (fromDataRegister.password !== fromDataRegister.confirmPassword) {
      toast.error("Passwords do not match!");
      setIsLoading(false);
      return;
    }

    // Call Register API here
    try{
      console.log("Register loaded: ", fromDataRegister);

      const response = await api.post("Users/Register", fromDataRegister);
      console.log('Register response: ', response.data.data);
      
      if(response.status === 200) {
        // Nếu có data trả về
        
          if(response.data.data){
            console.log('Register response data: ', response.data.data);
          }
          } else if (response.status === 204) {
            // No content nhưng thành công
            toast.success("Đăng nhập thành công!");
            navigate('/login'); // Chuyển hướng về trang chủ sau khi đăng nhập thành công
          }
          
          // Điều hướng về trang đăng nhập
          setIsActive(false);
          toast.success("Đăng ký thành công! Vui lòng đăng nhập.");
    }
    catch (err){
      console.error('Register error: ', err);
      toast.error(err.response.data);
    }
    finally{
      setIsLoading(false);
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
      <div className="auth-container" id="container" ref={containerRef}>
        {/* Login Form */}
        <div className="form-box login">
          <form onSubmit={handleLoginSubmit}>
            <h1 className="mb-4">Login</h1>
            
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
              placeholder="Password" 
              required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <div className="forgot-link mb-3">
              <a href="#" className="text-decoration-none">Forgot Password?</a>
            </div>

            <button 
            type="submit" 
            className="btn custom-btn w-100 mb-3"
            disabled={isLoading}
            >
              {isLoading ? "Logining...." : "Login"}
            </button>

            <p className="text-center mb-3">or Login with social platforms</p>

            <div className="social-icons d-flex justify-content-center gap-2">
              <a href="#" className="social-link">
                <i className="bi bi-google"></i>
              </a>
            </div>
          </form>
        </div>

        {/* Register Form */}
        <div className="form-box register">
          <form onSubmit={handleRegisterSubmit}>
            <h1 className="mb-4">Register</h1>
            
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
              placeholder="Password" 
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
              placeholder="Confirm Password" 
              required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <button 
            type="submit" 
            className="btn custom-btn w-100 mb-3"
            disabled={isLoading}
            >
              {isLoading ? "Registering...." : "Register"}
            </button>

            <p className="text-center mb-3">or Register with social platforms</p>

            <div className="social-icons d-flex justify-content-center gap-2">
              <a href="#" className="social-link">
                <i className="bi bi-google"></i>
              </a>
            </div>
          </form>
        </div>

        {/* Toggle Box */}
        <div className="toggle-box">
          <div className="toggle-panel toggle-left d-flex flex-column justify-content-center align-items-center text-center">
            <h1 className="mb-3">Hello, Welcome!</h1>
            <p className="mb-4">Don't have an account?</p>
            <button 
              className="btn toggle-btn" 
              onClick={handleRegisterClick}
            >
              Register
            </button>
          </div>
          
          <div className="toggle-panel toggle-right d-flex flex-column justify-content-center align-items-center text-center">
            <h1 className="mb-3">Welcome Back!</h1>
            <p className="mb-4">Already have an account?</p>
            <button 
              className="btn toggle-btn" 
              onClick={handleLoginClick}
            >
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginRegister;
