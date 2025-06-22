import React, { useEffect } from 'react';
import api from '../../config/axios.js';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import '../../css/index.css';
import { useState } from 'react';
import { toast } from 'react-toastify';
function Dashboard() {
    const navigate = useNavigate();
    const [isLoading, setIsLoading] = useState(false);
    const [dataUsers, setDataUsers] = useState([]);
    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';

    //API user
    const fetchData = async (e) => {
        setIsLoading(true);

        try{
            const response = await api.get('Users/GetAll');
            setDataUsers(response.data.data);
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
        fetchData();
    },[]);

  return (
    <div className='row g-0 bg-light min-vh-100 margin_top'>
        <aside className="col-md-3 col-lg-2 d-md-block bg-light sidebar border-end fixed-left text-center body_color_1">
            <a className="navbar-brand h1" href="/">
                GENCARE
            </a>
            <nav className='nav flex-column sidebar-nav mb-2 mt-2'>
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link mb-100" to="/dashboard">
                    <i className="bi bi-house-door-fill"></i> Dashboard
                </Link>
                </div>
                {isAdmin || isManager ?
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" href="/account">
                    <i className="bi bi-person-fill"></i> Users
                </Link>
                </div>
                : null}
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" href="/test-history">
                    <i className="bi bi-journal-text"></i> Status
                </Link>
                </div>
                
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" href="/book-appointment">
                    <i className="bi bi-calendar-plus-fill"></i> Book Appointment
                </Link>
                </div>
                
                {isStaff ?
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" to="/approve">
                    <i className="bi bi-check-circle-fill"></i> Approve Form
                </Link>
                </div>
                : null}
                <div className='mb-2 mt-2 tab_content_1'>
                <a className="nav-link" href='/'>
                    <i className="bi bi-box-arrow-right"></i> Back Home
                </a>
                </div>
            </nav>
        </aside>
        <main className="col-md-1 ms-sm-auto col-lg-10 px-md-4">
            <div className="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                <h1 className="h2">Dashboard</h1>
            </div>
            <div className="row">
                <div className="col-md-12">
                    {/* <h3>Welcome to your dashboard!</h3> */}
                </div>
            </div>
            <div className="d-flex gap-3 flex-wrap justify-content-start ">
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
            {/* <div className="row">
                <div className="col-md-4">
                    <div className="card mb-4">
                        <div className="card-body">
                            <h5 className="card-title">Account Information</h5>
                            <p className="card-text text-center">View and update your account details.</p>
                            <a href="/dashboard" className="btn btn-primary">Go to Account</a>
                        </div>
                    </div>
                </div>
                <div className="col-md-4">
                    <div className="card mb-4">
                        <div className="card-body">
                            <h5 className="card-title">Status</h5>
                            <p className="card-text text-center">View your past test results and history.</p>
                            <a href="/dashboard" className="btn btn-primary">View History</a>
                        </div>
                    </div>
                </div>
                <div className="col-md-4">
                    <div className="card mb-4">
                        <div className="card-body">
                            <h5 className="card-title">Book Appointment</h5>
                            <p className="card-text text-center">Schedule a new appointment for testing.</p>
                            <a href="/dashboard" className="btn btn-primary">Check Now</a>
                        </div>
                    </div>
                </div>
                <div className="col-md-4">
                    <div className="card mb-4">
                        <div className="card-body">
                            <h5 className="card-title">Approve Forms</h5>
                            <p className="card-text text-center">Review and approve submitted forms.</p>
                            <a href="/dashboard" className="btn btn-primary">Approve Forms</a>
                        </div>
                    </div>  
                </div>
            </div> */}
        </main>
            
    </div>
    
  );
}

export default Dashboard;