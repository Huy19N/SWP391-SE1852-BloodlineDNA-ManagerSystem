import { Link } from "react-router-dom";
import '../css/index.css';

function Sidebar (){
    const roleid = localStorage.getItem('roleId');
    const isStaff = roleid === '2';
    const isAdmin = roleid === '4';
    const isManager = roleid === '3';


    return (
                <div>
                    <a className="navbar-brand h1" href="/">
                        GENCARE
                    </a>
                    <nav className='nav flex-column sidebar-nav mb-2 mt-2'>
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link mb-100" to="dashboard">
                            <i className="bi bi-house-door-fill"></i> Dashboard
                        </Link>
                        </div>
                        {isAdmin || isManager ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="users">
                            <i className="bi bi-person-fill"></i> Users
                        </Link>
                        </div>
                        : null}
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="#">
                            <i className="bi bi-bookmark-check-fill"></i> Status
                        </Link>
                        </div>
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="approve">
                            <i className="bi bi-check-circle-fill"></i> Form
                        </Link>
                        </div>
                        : null}
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='dbbooking'>
                            <i className="bi bi-box2-fill"></i> Bookings
                        </Link>
                        </div>
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='#'>
                            <i className="bi bi-chat-left-text-fill"></i> Feedbacks
                        </Link>
                        </div>
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='#'>
                            <i className="bi bi-box"></i> Services
                        </Link>
                        </div>
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='#'>
                            <i className="bi bi-box"></i> Blogs 
                        </Link>
                        </div>
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='/'>
                            <i className="bi bi-door-open-fill"></i> Back Home
                        </Link>
                        </div>
                    </nav>
                </div>        
    );
}

export default Sidebar;