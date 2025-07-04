import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import api from '../../config/axios.js';
import { ToastContainer, toast } from 'react-toastify';
import Home from '../Home.jsx';

    function PaymentSuccess() {
        const [isLoading, setIsLoading] = useState(false);
        const [dataPayment, setDataPayment] = useState([]);
        
        const fetchPayment = async (e) => {
            setIsLoading(true);

            try{
                const [resPay, resService] = await Promise.all ([
                    api.get('Payment/GetALl'),
                    // api.get('')
                ]);
                const dataPay = resPay.data.data;

                setDataPayment(dataPay);
                console.log(dataPay);
            }
            catch(error){
                console.log(error.dataPay);
                toast.error("Error Data Payment");
            }
        }

        useEffect (() => {
            fetchPayment();
        },[])

        return (
            <div>
                <div style={{ padding: "40px", textAlign: "center" }}>
                    <h1 className="text-success">Payment Successfully!</h1>
                    <p><strong>Payment ID:</strong> {dataPayment.paymentId || " "}</p>
                    <p><strong>Booking ID:</strong> {dataPayment.bookingId}</p>
                    <p><strong>Customer Name:</strong> {dataPayment.paymentDate}</p>
                    <p><strong>Order Description:</strong></p>
                    <p><strong>Amount :</strong> VND</p>
                    <p><strong>Order Type :</strong> </p>
                </div>
                <div>
                    <button className="btn btn-primary">
                        <a className="text-light text-decoration-none" href="/">
                        Back Home
                        </a>
                    </button>
                </div>
            </div>
        );
}

export default PaymentSuccess;
