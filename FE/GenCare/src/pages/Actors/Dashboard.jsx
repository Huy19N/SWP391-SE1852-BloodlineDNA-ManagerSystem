import api from '../../config/axios.js';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import '../../css/index.css';
function Dashboard() {
    const navigate = useNavigate();





  return (
    <div className='row g-0 bg-light min-vh-100'>
        <aside className="col-md-3 col-lg-2 d-md-block bg-light sidebar border-end fixed-left text-center">
            <a className="navbar-brand " href="/">
                GenCare
            </a>
            <nav className='nav flex-column sidebar-nav mb-2 mt-2'>
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link mb-100" to="/dashboard">
                    <i className="bi bi-house-door-fill"></i> Dashboard
                </Link>
                </div>
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" href="/account">
                    <i className="bi bi-person-fill"></i> Users
                </Link>
                </div>
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
                <div className='mb-2 mt-2 tab_content_1'>
                <Link className="nav-link" href="/approve">
                    <i className="bi bi-check-circle-fill"></i> Approve Form
                </Link>
                </div>
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
                    <h3>Welcome to your dashboard!</h3>
                    <p>Here you can manage your account, view test history, book appointments, and approve forms.</p>
                </div>
            </div>
            <div className="row">
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
                            <a href="/dashboard" className="btn btn-primary">Book Now</a>
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
            </div>
        </main>
            
    </div>
    
  );
}

export default Dashboard;