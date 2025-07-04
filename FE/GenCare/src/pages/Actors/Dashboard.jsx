import React, { useEffect } from 'react';
import api from '../../config/axios.js';
import { data, Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import { useState } from 'react';
import { toast } from 'react-toastify';
import Profitchart from '../Chartjs/ProfitChart.jsx';
import UserChart from '../Chartjs/UserChart.jsx';
function Dashboard() {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const [dataUsers, setDataUsers] = useState([]);
    const [dataBooking, setDataBooking] = useState([]);
    const [dataTotalamount, setdataTotalamount] = useState([]);



    //totalmout có chia ra năm tháng và quý và ngày, 1 là năm , 2 là quý , 3 là tháng, 4 là ngày 
    const type = 1;
    //API user
    const fetchDataUser = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get('Users/GetAll');
            const users = response.data.data;
            setDataUsers(users);
            console.log("Data response: ", users);


        }
        catch(error){
            console.error("Load error:",error);
            toast.error(error.response.data);
        }
        finally{
            setIsLoading(false);
        }
    }

    const fetchDataBooking = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get('Bookings/GetAll');
            setDataBooking(response.data.data);
            console.log("Data response: ", response.data.data);
        }
        catch(error){
            console.error("Load error:",error);
            toast.error(error.response.data);
        }
        finally{
            setIsLoading(false);
        }
    }

    const fetchDataTotal = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get(`Payment/SumAmount/${type}`);
            setdataTotalamount(response.data.data);
            console.log("Data response: ", response.data.data);
        }
        catch(error){
            console.error("Load error:",error);
            toast.error(error.response.data);
        }
        finally{
            setIsLoading(false);
        }
    }

    // API 

    useEffect(() => {
        fetchDataUser();
        fetchDataBooking();
        fetchDataTotal();
    },[]);

  return (
    <div>
            <div className="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                <h1 className="h2">Dashboard</h1>
            </div>
            <div className="row">
                <div className="col-md-12">
                    {/* <h3>Welcome to your dashboard!</h3> */}
                </div>
            
                <div className="col-md-4 d-flex gap-3 flex-wrap justify-content-start ">
                    <div
                        className="card shadow-sm p-4"
                        style={{
                        minWidth: "250px",
                        borderBottom: "5px solid #4a90e2",
                        background: "#f5f8ff",
                        }}
                    >
                        <h6 className="text-muted">Total Users</h6>
                        <h3 className="mt-2 fw-bold">
                        <i class="bi bi-people-fill text-primary fs-4">   </i>{isLoading ? "Loading..." : dataUsers.length}
                        </h3>
                    </div>
                </div>

                <div className="col-md-4 d-flex gap-3 flex-wrap justify-content-start ">
                    <div
                        className="card shadow-sm p-4"
                        style={{
                        minWidth: "250px",
                        borderBottom: "5px solid #4a90e2",
                        background: "#f5f8ff",
                        }}
                    >
                        <h6 className="text-muted">Total Booking</h6>
                        <h3 className="mt-2 fw-bold">
                        <i class="bi bi-clipboard-check-fill text-primary fs-4"></i>      {isLoading ? "Loading..." : dataBooking.length}
                        </h3>
                    </div>
                </div>

                <div className="col-md-4 d-flex gap-3 flex-wrap justify-content-start ">
                    <div
                        className="card shadow-sm p-4"
                        style={{
                        minWidth: "250px",
                        borderBottom: "5px solid #4a90e2",
                        background: "#f5f8ff",
                        }}
                    >
                        <h6 className="text-muted">Total Profit</h6>
                        <h3 className="mt-2 fw-bold">
                        <i class="bi bi-bank text-primary fs-4"></i>      {isLoading ? "Loading..." : dataTotalamount}
                        </h3>
                    </div>
                </div>
                <div className='col-md-8 d-flex gap-2 flex-wrap justify-content-start'>
                    <Profitchart />
                </div>
                <div className='col-md-4 d-flex gap-2 flex-wrap justify-content-start'>
                    <UserChart />
                </div>
                
            </div>
    </div>
    
  );
}

export default Dashboard;