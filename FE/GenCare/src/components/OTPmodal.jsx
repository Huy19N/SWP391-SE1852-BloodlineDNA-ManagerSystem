import React, { useState, useRef, useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import api from '../config/axios.js';
import '../css/index.css';

const OTPModal = ({ show, email, onClose, onVerified }) => {
  const [code, setCode] = useState(new Array(6).fill(''));
  const inputRefs = useRef([]);

  // Xử lý nhập ký tự (chữ + số)
  const handleChange = (element, index) => {
    if (element.value === ' ') {
      element.value = '';
      return;
    }
    const newCode = [...code];
    newCode[index] = element.value;
    setCode(newCode);

    if (element.value && index < 4) {
      inputRefs.current[index + 1]?.focus();
    }
  };

  // Xử lý Backspace
  const handleKeyDown = (e, index) => {
    if (e.key === 'Backspace' && !code[index] && index > 0) {
      inputRefs.current[index - 1]?.focus();
    }
  };

  // Xử lý paste
  const handlePaste = e => {
    e.preventDefault();
    const pasteData = e.clipboardData.getData('text').slice(0, 6);
    const newCode = pasteData.split('');
    while (newCode.length < 6) newCode.push('');
    setCode(newCode);

    const lastFullInput = Math.min(pasteData.length - 1, 6);
    inputRefs.current[lastFullInput]?.focus();
  };

  useEffect(() => {
    inputRefs.current[0]?.focus();
  }, []);

  // Hàm gửi xác minh
  const handleVerify = async () => {
    const OTP = code.join('').trim();
    const Email = email.trim();

    if (!OTP) {
      toast.warning("Please enter the verification code!");
      return;
    }

    try {
      const res = await api.get(`VerifyEmail/ConfirmEmail`, {
        params: { email: Email, otp: OTP }
      });

      if (res.status === 200) {
        toast.success('Email verified successfully!');
        onVerified();
      }
    } catch (error) {
      toast.error('Invalid or expired code!');
    }
  };

  return (
    <Modal
      show={show}
      onHide={onClose}
      backdrop="static"
      centered
      dialogClassName="otp-modal"
    >
      <Modal.Body>
        {/* Icon ổ khóa */}
        <div className="otp-lock-icon">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24"
            strokeWidth={1.5} stroke="currentColor"
            className="otp-lock-svg">
            <path strokeLinecap="round" strokeLinejoin="round"
              d="M16.5 10.5V6.75a4.5 4.5 0 10-9 0v3.75M5.25 10.5h13.5a2.25 2.25 0 012.25 2.25v7.5A2.25 2.25 0 0118.75 22.5H5.25a2.25 2.25 0 01-2.25-2.25v-7.5a2.25 2.25 0 012.25-2.25z" />
          </svg>
        </div>

        {/* Tiêu đề */}
        <h1 className="otp-title">Đăng ký với xác nhận 2 bước</h1>
        <p className="otp-subtitle">Chúng tôi đã gửi mã về mail của bạn</p>

        {/* Input OTP */}
        <div className="otp-input-container" onPaste={handlePaste}>
          {code.map((data, index) => (
            <input
              key={index}
              ref={el => inputRefs.current[index] = el}
              type="text"
              maxLength={1}
              value={data}
              placeholder="•"
              onChange={e => handleChange(e.target, index)}
              onKeyDown={e => handleKeyDown(e, index)}
              className="otp-input"
            />
          ))}
        </div>

        {/* Nút xác nhận */}
        <Button className="otp-button" onClick={handleVerify}>Xác Nhận</Button>

        {/* Resend */}
        {/* <p className="otp-resend">
          Didn't receive a code? <span className="otp-resend-link">Resent code</span>
        </p> */}
      </Modal.Body>
    </Modal>
  );
};

export default OTPModal;
