
function Header(){
    return (
        <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow ">
            <div className="container-fluid">
                <a className="navbar-brand">GeneCare</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <a className="nav-link text-dark">Home</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link text-dark">Privacy</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link text-dark">About</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link text-dark">Services</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link text-dark">Blog</a>
                        </li>
						<li className="nav-item">
                            <a className="nav-link text-dark">Login</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>

    );
}

export default Header;