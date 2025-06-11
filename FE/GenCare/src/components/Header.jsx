import { NavLink } from 'react-router-dom';

export default function Header(){
    return (
        <header>
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
                            <NavLink className="navbar-brand" to="/">Home</NavLink>
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
                </div>
            </div>
        </nav>
    </header>

    );
}
