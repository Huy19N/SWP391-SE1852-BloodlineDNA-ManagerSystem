import React, { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import api from '../config/axios.js';

    const OTPModal = ({ show, email, onClose, onVerified }) => {
    const [otp, setOtp] = useState('');

    const handleVerify = async () => {
        try {
        const res = await api.get(`VerifyEmail/ConfirmEmail`, {
            params: {
            email: email,
            key: otp
            }
        });

        if (res.status === 200) {
            toast.success('Xác minh email thành công!');
            onVerified();
        }
        } catch (error) {
        toast.error('Mã xác minh không đúng hoặc đã hết hạn!');
        }
    };

    return (
        <Modal show={show} onHide={onClose} backdrop="static">
        <Modal.Header closeButton>
            <Modal.Title>Nhập Mã OTP</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <input
            type="text"
            className="form-control"
            placeholder="Nhập mã OTP từ email"
            value={otp}
            onChange={(e) => setOtp(e.target.value)}
            />
        </Modal.Body>
        <Modal.Footer>
            <Button variant="secondary" onClick={onClose}>Hủy</Button>
            <Button variant="primary" onClick={handleVerify}>Xác Nhận</Button>
        </Modal.Footer>
        </Modal>
    );
};

export default OTPModal;
