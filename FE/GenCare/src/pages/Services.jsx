import React, { useEffect } from 'react';
import img1 from '../assets/ServicesHome.jpg';
import img2 from '../assets/staff.jpg';
import img3 from '../assets/test1.jpg';
import img4 from '../assets/test2.jpg';
import img5 from '../assets/girl4.jpg';
import img6 from '../assets/girl3.jpg';
import img7 from '../assets/princess.jpg';
import img8 from '../assets/hime.jpg';
import logo1 from '../assets/logo1.png';
import logo2 from '../assets/logo2.png';
import logo3 from '../assets/logo3.png';
import logo4 from '../assets/logo4.png';
import logo5 from '../assets/logo5.jpg';
import * as bootstrap from 'bootstrap';
import { Link } from 'react-router-dom';

function Services() {
      // Initialize Bootstrap carousels on component mount
      useEffect(() => {
        const carousels = document.querySelectorAll('.carousel');
        carousels.forEach((carousel) => {
          new bootstrap.Carousel(carousel, {
            ride: 'carousel',
          });
        });
      }, []);
  return (
    <>
      {/* Part1 */}
      <div className="container-fluid p-0 m-0 position-relative">
        {/* Carousel */}
        <div id="demo" className="carousel slide" data-bs-ride="carousel">
          {/* nút ảnh */}
          <div className="carousel-indicators">
            <button type="button" data-bs-target="#demo" data-bs-slide-to="0" className="active"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="1"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="2"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="3"></button>
          </div>

          {/* ảnh */}
          <div className="carousel-inner">
            <div className="carousel-item active position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img1} alt="Los Angeles" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img2} alt="Chicago" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img3} alt="New York 1" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img4} alt="New York 2" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
          </div>

          {/* nút trái phải */}
          <button className="carousel-control-prev" type="button" data-bs-target="#demo" data-bs-slide="prev">
            <span className="carousel-control-prev-icon"></span>
          </button>
          <button className="carousel-control-next" type="button" data-bs-target="#demo" data-bs-slide="next">
            <span className="carousel-control-next-icon"></span>
          </button>
        </div>

        {/* Overlay nội dung lên ảnh */}
        <div className="position-absolute top-50 start-50 translate-middle text-center text-white">
          <h1 className="fw-bold display-4 text-primary">DỊCH VỤ CỦA CHÚNG TÔI</h1>
        </div>
      </div>

      {/* loại dịch vụ */}
      <section className="text-center py-5" style={{ backgroundColor: "#fef9f4" }}>
        <div className="container-fluid mt-5 p-4 rounded shadow-lg border" style={{ background: "rgba(255, 255, 255, 0.9)", borderWidth: "3px", width: "80%" }}>
          <div>
            <h1 className="text-info fw-bold" style={{ fontSize: "3.5rem" }}>Các loại dịch vụ của chúng tôi</h1>
          </div>

          <div className="container my-5">
            <div className="row justify-content-center align-items-start text-center g-5">
              {/* BẢNG DÂN SỰ */}
              <div className="col-lg-6">
                <Link to="/civil-services" className="text-decoration-none text-dark">
                  <div className="card shadow border-0 custom-card">
                    <div className="card-header bg-primary text-white text-center fs-1 fw-bold">DÂN SỰ</div>
                    <div className="card-body">
                      <table className="table table-hover table-bordered custom-table fs-4 fw-bold mb-0">
                        <tbody>
                          <tr><td>
                            <Link to="/civil-services#Civil-Type-1" className="text-decoration-none d-block text-dark">
                              Loại 1
                            </Link>
                          </td></tr>
                          <tr><td>
                            <Link to="/civil-services#Civil-Type-2" className="text-decoration-none d-block text-dark">
                              Loại 2
                            </Link>
                          </td></tr>
                          <tr><td>
                            <Link to="/civil-services#Civil-Type-3" className="text-decoration-none d-block text-dark">
                              Loại 3
                            </Link>
                          </td></tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </Link>
              </div>

              {/* BẢNG PHÁP LÝ */}
              <div className="col-lg-6">
                <Link to="/legal-services" className="text-decoration-none text-dark">
                  <div className="card shadow border-0 custom-card">
                    <div className="card-header bg-primary text-white text-center fs-1 fw-bold">PHÁP LÝ</div>
                    <div className="card-body">
                      <table className="table table-hover table-bordered custom-table fs-4 fw-bold mb-0">
                        <tbody>
                          <tr><td>Loại 1</td></tr>
                          <tr><td>Loại 2</td></tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </Link>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* DỊCH VỤ GENECARE */}
      <div className="container-fluid mt-5 mb-5 p-4 rounded shadow" style={{ backgroundColor: "#CCEFF6", width: "76%" }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: "1px" }}></div>
          <h2 className="mx-4 text-primary text-center">DỊCH VỤ XÉT NGHIỆM ADN TẠI GENECARE</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: "1px" }}></div>
        </div>

        <div className="container my-5">
          <div className="row justify-content-center align-items-center text-center g-4">

            <div className="col-md-4 rounded-3">
              <a href="/civil" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/Images/test1.jpg" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm cha/mẹ-con</h3>
                  </div>
                </div>
              </a>
            </div>

            <div className="col-md-4 rounded-3">
              <a href="/legal" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0">
                  <img src="/Images/test1.jpg" className="card-img-top" alt="Xét nghiệm dòng họ" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm dòng họ</h3>
                  </div>
                </div>
              </a>
            </div>

            <div className="col-md-4 rounded-3">
              <a href="/civil" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0">
                  <img src="/Images/test1.jpg" className="card-img-top" alt="Xét nghiệm thất lạc" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm tìm người thất lạc</h3>
                  </div>
                </div>
              </a>
            </div>

          </div>
        </div>
      </div>
    </>
  );
}

export default Services;
