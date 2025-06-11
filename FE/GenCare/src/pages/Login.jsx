import React, { useState, useRef, useEffect } from 'react';
import '../css/login.css';

const LoginRegister = () => {
  const [isActive, setIsActive] = useState(false);
  const containerRef = useRef(null);

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

  const handleLoginSubmit = (e) => {
    e.preventDefault();
    // Handle login logic here
    console.log('Login submitted');
  };

  const handleRegisterSubmit = (e) => {
    e.preventDefault();
    // Handle register logic here
    console.log('Register submitted');
  };

  return (
    <div className="d-flex justify-content-center align-items-center min-vh-100" style={{background: 'linear-gradient(90deg,#e2e2e2, #c9d6ff)'}}>
      <div className="auth-container" id="container" ref={containerRef}>
        {/* Login Form */}
        <div className="form-box login">
          <form onSubmit={handleLoginSubmit}>
            <h1 className="mb-4">Login</h1>
            
            <div className="input-box mb-4">
              <input type="text" className="form-control custom-input" placeholder="Email" required />
              <i className="bi bi-envelope-fill input-icon"></i>
            </div>

            <div className="input-box mb-3">
              <input type="password" className="form-control custom-input" placeholder="Password" required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <div className="forgot-link mb-3">
              <a href="#" className="text-decoration-none">Forgot Password?</a>
            </div>

            <button type="submit" className="btn custom-btn w-100 mb-3">
              Login
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
              <input type="email" className="form-control custom-input" placeholder="Email" required />
              <i className="bi bi-envelope-fill input-icon"></i>
            </div>

            <div className="input-box mb-3">
              <input type="password" className="form-control custom-input" placeholder="Password" required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <div className="input-box mb-4">
              <input type="password" className="form-control custom-input" placeholder="Confirm Password" required />
              <i className="bi bi-lock-fill input-icon"></i>
            </div>

            <button type="submit" className="btn custom-btn w-100 mb-3">
              Register
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
