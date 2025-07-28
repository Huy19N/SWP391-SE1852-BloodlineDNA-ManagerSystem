import React from 'react';

function Footer(){
    
    return (
        <>
        <footer className="bg-light text-dark pt-5 pb-4">
            <div className="container text-md-left">
                <div className="row">

                    <div className="col-md-3 col-lg-3 col-xl-3 mx-auto mb-4">
                        <h2 className="fw-bold mb-3">GenCare</h2>
                        <p>Công ty TNHH Thương mại và Dịch vụ DNA Testings (Công ty trách nhiệm hữu hạn bốn thành viên)</p>
                        <p>
                            <i className="bi bi-telephone-fill"></i> 09X-XXX-XXXX<br />
                            <i className="bi bi-telephone-fill"></i> 09X-XXX-XXXX
                        </p>
                        <p><i className="bi bi-geo-alt-fill"></i>Số 11, Đường Số 49, P. Hiệp Bình Chánh, TP. Thủ Đức, TP. HCM</p>
                    </div>

                    
                    <div className="col-md-2 col-lg-2 col-xl-2 mx-auto mb-4">
                        <h6 className="text-uppercase fw-bold mb-4">Đường Dẫn Quan Trọng</h6>
                        <ul className="list-unstyled">
                            <li><a href="#home" className="text-reset text-decoration-none">Trang Chủ</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Điều Khoản</a></li>
                            <li><a href="#about" className="text-reset text-decoration-none">Về Chúng Tôi</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Dịch Vụ</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Tin Nhắn</a></li>
                        </ul>
                    </div>

                    
                    <div className="col-md-3 col-lg-3 col-xl-3 mx-auto mb-4">
                        <h6 className="text-uppercase fw-bold mb-4">Phương Pháp</h6>
                        <ul className="list-unstyled">
                            <li><a href="#" className="text-reset text-decoration-none">Chăm Sóc Tinh thần</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Hệ thống Rx</a></li>
                            <li><a href="#" className="text-reset text-decoration-none">Nhân viên xét nghiệm</a></li>
                            <li><a href="#" className="text-reset text-decoration-none"></a></li> 
                        </ul>
                    </div>

                    
                    <div className="col-md-4 col-lg-4 col-xl-4 mx-auto mb-4">
                    <h6 className="text-uppercase fw-bold mb-4">Cổng thông tin liên quan</h6>
                        <div>
                            <a href="#" className="me-3 text-reset"><i className="bi bi-facebook fs-4"></i></a>
                            <a href="#" className="me-3 text-reset"><i className="bi bi-instagram fs-4"></i></a>
                            <a href="#" className="text-reset"><i className="bi bi-linkedin fs-4"></i></a>
                        </div>
                    </div>

                </div>
            </div>
            <div className="container">
                &copy; 2025 - GeneCare - <a asp-area="" asp-controller="Home" asp-action="Privacy">Điều Khoản</a>
            </div>
        </footer>
        </>
    );
}

export default Footer;