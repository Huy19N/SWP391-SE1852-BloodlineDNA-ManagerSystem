import { useEffect } from 'react';
import * as bootstrap from 'bootstrap';
import {  useNavigate } from 'react-router-dom';
import img1 from '../assets/ServicesHome.jpg';
import img2 from '../assets/staff.jpg';
import img3 from '../assets/test1.jpg';
import img4 from '../assets/test2.jpg';

function Services() {
  const navigate = useNavigate();

  useEffect(() => {
    const carousels = document.querySelectorAll('.carousel');
    carousels.forEach((carousel) => {
      new bootstrap.Carousel(carousel, {
        ride: 'carousel',
      });
    });
  }, []);

  const handleSelect = (mainType) => {
    localStorage.setItem(
      "selectedService",
      JSON.stringify({ mainType })
    );

    // Chuyển trang đúng theo loại
    if (mainType === 'dân sự') {
      navigate('/civil-services');
    } else if (mainType === 'pháp lý') {
      navigate('/legal-services');
    }
  };

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
              <img src={img1} alt="Slide 1" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img2} alt="Slide 2" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img3} alt="Slide 3" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: "16/6" }}>
              <img src={img4} alt="Slide 4" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
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
              {/* DÂN SỰ */}
              <div className="col-lg-6">
                <div className="text-decoration-none text-dark" onClick={() => handleSelect('dân sự')}>
                  <div className="card shadow border-0 custom-card">
                    <div className="card-header bg-primary text-white text-center fs-1 fw-bold">DÂN SỰ</div>
                  </div>
                </div>
              </div>

              {/* PHÁP LÝ */}
              <div className="col-lg-6">
                <div className="text-decoration-none text-dark" onClick={() => handleSelect('pháp lý')}>
                  <div className="card shadow border-0 custom-card">
                    <div className="card-header bg-primary text-white text-center fs-1 fw-bold">PHÁP LÝ</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </>
  );
}

export default Services;
