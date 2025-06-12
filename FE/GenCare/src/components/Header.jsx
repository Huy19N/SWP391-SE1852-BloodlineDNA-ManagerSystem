import { NavLink } from 'react-router-dom';
import React, { useState, useEffect } from 'react';
//import '../css/LightMode.css';

export default function Header(){
    const [darkMode, setDarkMode] = useState(false);

    useEffect(() => {
        if (darkMode) {
            document.body.classList.add('dark-mode');
        } else {
            document.body.classList.remove('dark-mode');
        }
    }, [darkMode]);

    return (
        <header className='fixed-top'>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow ">
            <div className="container-fluid">
                <NavLink className="navbar-brand" to="/">GeneCare</NavLink>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <NavLink className="navbar-brand " to="/">Home</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="navbar-brand" to="/">Privacy</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="navbar-brand" to="/">About</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="navbar-brand" to="/services">Services</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="navbar-brand" to="/">Blog</NavLink>
                        </li>
						<li className="nav-item">
                            <NavLink className="navbar-brand" to="/login">Login</NavLink>
                        </li>
                    </ul>
                    {/*nút chế độ sáng tối */}
                    <p>{'chưa xong không đụng giúp tao ==>>>'} </p>
                    <button className="btn btn-outline-secondary ms-2"
                            onClick={() => setDarkMode(!darkMode)}
                            >
                            {darkMode ? "☀ Light Mode" : "🌙 Dark Mode"}
                    </button>
                </div>
            </div>
        </nav>
    </header>

    );
}
