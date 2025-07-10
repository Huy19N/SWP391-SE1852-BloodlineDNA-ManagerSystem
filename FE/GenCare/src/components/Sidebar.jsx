import { Link } from "react-router-dom";
import { useState } from 'react';
import '../css/index.css';

function Sidebar (){

    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const toggleDropdown = () => setIsDropdownOpen(!isDropdownOpen);

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
                        {isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="users">
                            <i className="bi bi-person-fill"></i> Users
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="approve">
                            <i className="bi bi-check-circle-fill me-2"></i> Form
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='dbbooking'>
                            <i className="bi bi-box2-fill me-2"></i> Bookings
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="#">
                            <i className="bi bi-bookmark-check-fill me-2"></i> Status
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to="results">
                            <i class="bi bi-clipboard2-check-fill me-2"></i> Results
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='#'>
                            <i className="bi bi-journal-text me-2"></i> Blogs
                        </Link>
                        </div>
                        : null}
                        {isStaff || isManager || isAdmin ?
                        <div className='mb-2 mt-2 tab_content_1'>
                        <Link className="nav-link" to='#'>
                            <i className="bi bi-chat-left-text-fill me-2"></i> Feedbacks
                        </Link>
                        </div>
                        : null}
                        {isAdmin || isManager ?
                        <div className={`click-dropdown tab_content_1`}>
                        <span className="nav-link dropdown-toggle" onClick={toggleDropdown}>
                            More
                        </span>
                        {/* Dropdown nội dung sẽ đẩy xuống */}
                        {isDropdownOpen && (
                            <ul className="slide-dropdown-content custom-dropdown">
                            <li><Link className="dropdown-item" to="services"><i className="bi bi-box me-2"></i> Services</Link></li>
                            <li><Link className="dropdown-item" to="price"><i className="bi bi-box me-2"></i> Services Price</Link></li>
                            <li><Link className="dropdown-item" to="collectionmethod"><i className="bi bi-box me-2"></i> CollectionMethod</Link></li>
                            <li><Link className="dropdown-item" to="durations"><i className="bi bi-box me-2"></i> Durations</Link></li>
                            <li><Link className="dropdown-item" to="samples"><i className="bi bi-box me-2"></i> Samples</Link></li>
                            <li><Link className="dropdown-item" to="steptest"><i className="bi bi-box me-2"></i> Step Test</Link></li>
                            <li><Link className="dropdown-item" to="status"><i className="bi bi-box me-2"></i> Status</Link></li>
                            </ul>
                        )}
                        </div>
                        : null}
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