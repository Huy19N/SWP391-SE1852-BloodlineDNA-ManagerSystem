import React, { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { toast } from 'react-toastify';
import '../css/index.css';
import api from '../config/axios.js';

    const OTPModal = ({ show, email, onClose, onVerified }) => {
    const [otp, setOtp] = useState('');

    const handleVerify = async () => {

        const OTP = otp.trim();
        const Email = email.trim();

        if(!OTP){
            toast.warning("Vui lòng nhập mã OTP!");
            return;
        }

        try {
        const res = await api.get(`VerifyEmail/ConfirmEmail`, {
            params: {
            email: Email,
            otp: OTP,
            }
        });

        console.log("Gửi xác minh email:", Email);
        console.log("Với mã OTP:", OTP);

        if (res.status === 200) {
            toast.success('Xác minh email thành công!');
            onVerified();
        }
        } catch (error) {
        console.log(error.response?.data || error.message);
        toast.error('Mã xác minh không đúng hoặc đã hết hạn!');
        }
    };

    return (
        <Modal className='justify-center aglin-center' show={show} onHide={onClose} backdrop="static">
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
                <Button variant="primary" onClick={handleVerify}>Xác Nhận</Button>
            </Modal.Footer>
        </Modal>
    );
};

export default OTPModal;
