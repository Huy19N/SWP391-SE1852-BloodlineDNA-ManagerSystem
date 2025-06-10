
function Footer(){
    
    return (
        <>
        <footer className="bg-light text-dark pt-5 pb-4">
            <div className="container text-md-left">
                <div className="row">

                    <div className="col-md-3 col-lg-3 col-xl-3 mx-auto mb-4">
                        <h2 className="fw-bold mb-3">GenCare</h2>
                        <p>DNA Testings Trading & Service Co., Ltd (Four-Member Limited Liability Company)</p>
                        <p>
                            <i className="bi bi-telephone-fill"></i> 09X-XXX-XXXX
                            <i className="bi bi-telephone-fill"></i> 09X-XXX-XXXX
                        </p>
                        <p><i className="bi bi-geo-alt-fill"></i>Số 11, Đường Số 49, P. Hiệp Bình Chánh, TP. Thủ Đức, TP. HCM</p>
                    </div>

                    
                    <div className="col-md-2 col-lg-2 col-xl-2 mx-auto mb-4">
                        <h6 className="text-uppercase fw-bold mb-4">Important Links</h6>
                        <ul className="list-unstyled">
                            <li><a href="#home" className="text-reset text-decoration-none">Home</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Privacy</a></li>
                            <li><a href="#about" className="text-reset text-decoration-none">About Us</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Services</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Blog</a></li>
                        </ul>
                    </div>

                    
                    <div className="col-md-3 col-lg-3 col-xl-3 mx-auto mb-4">
                        <h6 className="text-uppercase fw-bold mb-4">Solutions</h6>
                        <ul className="list-unstyled">
                            <li><a href="#" className="text-reset text-decoration-none">Health Care</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Mental Health Rx</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Staff Care</a></li>
                            <li><a href="#" className="text-reset text-decoration-none"></a></li> 
                        </ul>
                    </div>

                    
                    <div className="col-md-4 col-lg-4 col-xl-4 mx-auto mb-4">
                    <h6 className="text-uppercase fw-bold mb-4">Newsletter</h6>
                        <p>Subscribe newsletter for the latest offers and news</p>
                        <form className="input-group mb-3">
                            <input type="email" className="form-control" placeholder="Email mail" required></input>
                            <button className="btn btn-outline-secondary" type="submit">
                                <i className="bi bi-send-fill"></i>
                            </button>
                        </form>
                        <div>
                            <a href="#" className="me-3 text-reset"><i className="bi bi-facebook fs-4"></i></a>
                            <a href="#" className="me-3 text-reset"><i className="bi bi-instagram fs-4"></i></a>
                            <a href="#" className="text-reset"><i className="bi bi-linkedin fs-4"></i></a>
                        </div>
                    </div>

                </div>
            </div>
            <div className="container">
                &copy; 2025 - GeneCare - <a asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
            </div>
        </footer>
        </>
    );
}

export default Footer;