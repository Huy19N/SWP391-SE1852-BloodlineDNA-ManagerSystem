import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';
import img1 from '../assets/ServicesHome.jpg';
import img2 from '../assets/staff.jpg';
import img3 from '../assets/test1.jpg';
import img4 from '../assets/test2.jpg';
import img5 from '../assets/girl4.jpg';
import img6 from '../assets/girl3.jpg';
import img7 from '../assets/princess.jpg';
import img8 from '../assets/hime.jpg';
import img9 from '../assets/doctor1.jpg';
import logo1 from '../assets/logo1.png';
import logo2 from '../assets/logo2.png';
import logo3 from '../assets/logo3.png';
import logo4 from '../assets/logo4.png';
import logo5 from '../assets/logo5.jpg';
import * as bootstrap from 'bootstrap';

function Home() {
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
      {/* Part 1: Background Home Carousel */}
      <div className="container-fluid p-0 m-0" id="home">
        <div id="demo" className="carousel slide" data-bs-ride="carousel">
          <div className="carousel-indicators">
            <button type="button" data-bs-target="#demo" data-bs-slide-to="0" className="active"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="1"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="2"></button>
            <button type="button" data-bs-target="#demo" data-bs-slide-to="3"></button>
          </div>

          <div className="carousel-inner">
            <div className="carousel-item active position-relative w-100" style={{ aspectRatio: '16/6' }}>
              <img src={img1} alt="Los Angeles" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: '16/6' }}>
              <img src={img2} alt="Chicago" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: '16/6' }}>
              <img src={img3} alt="New York" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="carousel-item position-relative w-100" style={{ aspectRatio: '16/6' }}>
              <img src={img4} alt="New York" className="d-block position-absolute top-0 start-0 w-100 h-100 object-fit-cover" />
            </div>
            <div className="position-absolute top-50 start-50 translate-middle text-center text-white">
            </div>
          </div>

          <button className="carousel-control-prev" type="button" data-bs-target="#demo" data-bs-slide="prev">
            <span className="carousel-control-prev-icon"></span>
          </button>
          <button className="carousel-control-next" type="button" data-bs-target="#demo" data-bs-slide="next">
            <span className="carousel-control-next-icon"></span>
          </button>
        </div>
      </div>

      {/* Part 2: Text Advice */}
      <div className="container text-center mt-20 mb-10">
        <h2 className="fw-bold display-6 py-5">
          Chúng tôi <span className="text-primary">nỗ lực</span> bạn <span className="text-primary"> hài lòng </span><br />
          Chúng tôi <span className="text-primary">chính xác</span> bạn <span className="text-primary">hi vọng </span>
        </h2>
      </div>

      {/* Part 3: Why Choose Us */}
      <section className="py-5 bg-light">
        <div className="container my-5 bg-light">
          <div className="row align-items-center">
            <div className="col-md-6 mb-4 mb-md-0">
              <img src={img1} alt="Lab Image" className="img-fluid rounded shadow" />
            </div>
            <div className="col-md-6">
              <h2 className="fw-bold mb-4">
                Đây là lý do tại sao việc <span className="text-primary">chọn chúng tôi </span>là quyết định đúng đắng mà bạn có thể <span className="text-primary">tin tưởng</span>
              </h2>
              <div className="d-flex mb-4">
                <i className="bi bi-lightbulb-fill text-primary fs-2 me-3"></i>
                <div>
                  <h5 className="text-primary fw-bold mb-1">Công nghệ kiểm tra ADN tiên tiến</h5>
                  <p className="mb-0 text-muted">Chúng tôi sử dụng công nghệ hiện đại để cung cấp các phương pháp xét nghiệm ADN chính xác và đáng tin cậy</p>
                </div>
              </div>
              <div className="d-flex mb-4">
                <i className="bi bi-emoji-smile text-primary fs-2 me-3"></i>
                <div>
                  <h5 className="text-primary fw-bold mb-1">Sự hài lòng của khách hàng là ưu tiên của chúng tôi</h5>
                  <p className="mb-0 text-muted">Những khách hàng của chúng tôi luôn thể hiện sự hài lòng với chất lượng dịch vụ mà chúng tôi đem đến</p>
                </div>
              </div>
              <div className="d-flex">
                <i className="bi bi-people-fill text-primary fs-2 me-3"></i>
                <div>
                  <h5 className="text-primary fw-bold mb-1">Đội ngũ y bác sĩ đáng tin cậy</h5>
                  <p className="mb-0 text-muted">Chúng tôi được công nhận là có đội ngũ y bác sĩ đáng tin cậy và nổi tiếng trong lĩnh vực xét nghiệm ADN</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Part 4: Our Process */}
      <section className="text-center py-5">
        <div className="container">
          <h2 className="fw-bold display-5">
            Qui trình của chúng tôi: <span className="text-info">phần mềm </span> đăng ký trực tuyến
          </h2>
          <p className="text-muted">
             Tiết kiệm thời gian và chi phí, dễ dàng, giảm thiểu rủi ro và hiệu quả
          </p>
          <div
            className="d-flex justify-content-between align-items-start flex-wrap position-relative mt-5"
            style={{
              backgroundImage: "url('/path-to-your-dotted-line.svg')", // Replace with actual path
              backgroundRepeat: 'no-repeat',
              backgroundPosition: 'center',
              backgroundSize: 'cover',
            }}
          >
            <div className="text-center" style={{ width: '15%' }}>
              <i className="bi bi-person-check-fill fs-1 text-info"></i>
              <p className="mt-2">Cần xét nghiệm<br />DNA</p>
            </div>
            <i className="bi bi-arrow-right-circle fs-1 text-info"></i>
            <div className="text-center" style={{ width: '15%' }}>
              <i className="bi bi-calendar3 fs-1 text-info"></i>
              <p className="mt-2">Đặt lịch <br />hẹn</p>
            </div>
            <i className="bi bi-arrow-right-circle fs-1 text-info"></i>
            <div className="text-center" style={{ width: '15%' }}>
              <i className="bi bi-droplet-fill fs-1 text-info"></i>
              <p className="mt-2">Thu Thập<br />Mẫu</p>
            </div>
            <i className="bi bi-arrow-right-circle fs-1 text-info"></i>
            <div className="text-center" style={{ width: '15%' }}>
              <i className="bi bi-pc-display fs-1 text-info"></i>
              <p className="mt-2">Xử lý<br />DNA</p>
            </div>
            <i className="bi bi-arrow-right-circle fs-1 text-info"></i>
            <div className="text-center" style={{ width: '15%' }}>
              <i className="bi bi-bar-chart-line-fill fs-1 text-info"></i>
              <p className="mt-2">Báo cáo dữ liệu<br />thời gian thực</p>
            </div>
          </div>
        </div>
      </section>

      {/* Part 5: About */}
      <section className="py-5 bg-light" id="about">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-md-6">
              <h6 className="fw-bold text-muted">Về chúng tôi</h6>
              <h2 className="fw-bold mb-4">GenCare-DNA<br />Trung tâm chuẩn đoán</h2>
              <p>
                Trung tâm DNA TESTINGS tự hào là một trong những đơn vị chuyên xét nghiệm ADN ở TP. Hồ Chí Minh và Hà Nội...
              </p>
              <p>
                Với đội ngũ chuyên gia cao cấp nhiều năm kinh nghiệm được đào tạo chuyên nghiệp...
              </p>
              <a href="#" className="btn btn-danger rounded-pill px-4 mt-3">Liên hệ</a>
            </div>
            <div className="col-md-6 position-relative mt-4 mt-md-0">
              <img src={img3} alt="DNA" className="img-fluid rounded shadow" />
              <img
                src={img4}
                alt="DNA"
                className="img-fluid rounded shadow position-absolute"
                style={{ width: '60%', bottom: '-20px', left: '-25px' }}
              />
            </div>
          </div>
        </div>
      </section>

      {/* Part 6: Our Team */}
      <section className="py-5 bg-white">
        <div className="container text-center">
          <h6 className="text-muted">Nhóm của chúng tôi</h6>
          <h2 className="fw-bold mb-5">Cố vấn chuyên gia</h2>
          <div className="row g-4">
            <div className="col-md-3">
              <div className="card shadow-lg border-0 py-4 px-3">
                <img
                  src={img9}
                  alt="TS BS Nguyễn Văn Thành Đạt"
                  className="rounded-circle border border-3 border-danger mx-auto"
                  style={{ width: '120px', height: '120px', objectFit: 'cover' }}
                />
                <div className="mt-3">
                  <h6 className="fw-bold">TS BS Nguyễn Văn Thành Đạt</h6>
                  <p className="text-muted small text-center">TS BS Viện Trưởng</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card shadow-lg border-0 py-4 px-3">
                <img
                  src={img9}
                  alt="Nguyễn Trần Hiếu Thuận"
                  className="rounded-circle border border-3 border-danger mx-auto"
                  style={{ width: '120px', height: '120px', objectFit: 'cover' }}
                />
                <div className="mt-3">
                  <h6 className="fw-bold">Bác sĩ Nguyễn Trần Hiếu Thuận</h6>
                  <p className="text-muted small text-center">Trưởng Lab TP. HCM</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card shadow-lg border-0 py-4 px-3">
                <img
                  src={img9}
                  alt="TS Huỳnh Trung Kiên"
                  className="rounded-circle border border-3 border-danger mx-auto"
                  style={{ width: '120px', height: '120px', objectFit: 'cover' }}
                />
                <div className="mt-3">
                  <h6 className="fw-bold">PGS. TS Huỳnh Trung Kiên</h6>
                  <p className="text-muted small text-center">Cố vấn Cao Cấp</p>
                </div>
              </div>
            </div>
            <div className="col-md-3">
              <div className="card shadow-lg border-0 py-4 px-3">
                <img
                  src={img9}
                  alt="Nguyễn Gia Huy"
                  className="rounded-circle border border-3 border-danger mx-auto"
                  style={{ width: '120px', height: '120px', objectFit: 'cover' }}
                />
                <div className="mt-3">
                  <h6 className="fw-bold">CN. Nguyễn Gia Huy</h6>
                  <p className="text-muted small text-center">Giám đốc chi nhánh TP.HCM</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Part 7: Customer Feedback */}
      <section className="py-5 position-relative bg-light">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-md-6 position-relative mb-5 mb-md-0">
              <img src={img3} alt="DNA" className="img-fluid rounded shadow" style={{ transform: 'translateX(-8%)' }} />
              <img
                src={img4}
                alt="DNA"
                className="img-fluid rounded shadow position-absolute img-overlay"
                style={{ width: '50%', top: '-30px', right: 0, transform: 'translateX(-138%)' }}
              />
              <img
                src={img4}
                alt="DNA"
                className="img-fluid rounded shadow position-absolute img-overlay"
                style={{ width: '50%', bottom: '-20px', right: 0, transform: 'translateX(3%)' }}
              />
            </div>
            <div className="col-md-6 text-center">
              <h5 className="text-secondary mb-2">PHẢN HỒI TỪ KHÁCH HÀNG CỦA CHÚNG TÔI</h5>
              <h2 className="fw-bold mb-5">Họ đã nói gì?</h2>
              <div id="customerFeedbackCarousel" className="carousel slide" data-bs-ride="carousel">
                <div className="carousel-inner">
                  <div className="carousel-item active">
                    <div className="card shadow mx-auto p-4" style={{ maxWidth: '600px' }}>
                      <p className="mb-4">
                        DNA Tesing là địa chỉ đáng tin cậy cho các dự án nghiên cứu và ứng dụng công nghệ di truyền...
                      </p>
                      <div className="d-flex align-items-center">
                        <img src={img5} alt="avatar" className="rounded-circle me-3" style={{ width: '50px', height: '50px' }} />
                        <div className="text-start">
                          <h6 className="mb-0 text-danger">Kiều Oanh</h6>
                          <small className="text-muted">Quận 10 - TP. HCM</small>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div className="carousel-item">
                    <div className="card shadow mx-auto p-4" style={{ maxWidth: '600px' }}>
                      <p className="mb-4">
                        Tôi rất hài lòng với chất lượng dịch vụ. Đội ngũ chuyên nghiệp và tận tâm.
                      </p>
                      <div className="d-flex align-items-center">
                        <img src={img6} alt="avatar" className="rounded-circle me-3" style={{ width: '50px', height: '50px' }} />
                        <div className="text-start">
                          <h6 className="mb-0 text-danger">Vân Anh</h6>
                          <small className="text-muted">Hà Nội</small>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div className="mt-4">
                  <button type="button" data-bs-target="#customerFeedbackCarousel" data-bs-slide-to="0" className="active btn btn-sm rounded-circle me-2 bg-secondary"></button>
                  <button type="button" data-bs-target="#customerFeedbackCarousel" data-bs-slide-to="1" className="btn btn-sm rounded-circle bg-secondary"></button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Part 8: Business Partners */}
      <section className="bg-primary p-2 bg-opacity-75 py-4">
        <div className="container">
          <div id="partnerCarousel" className="carousel slide" data-bs-ride="carousel">
            <div className="carousel-inner text-center">
              <div className="carousel-item active">
                <div className="d-flex justify-content-center gap-4 flex-wrap">
                  <img src={logo1} alt="Logo 1" className="partner-logo" />
                  <img src={logo2} alt="Logo 2" className="partner-logo" />
                  <img src={logo3} alt="Logo 3" className="partner-logo" />
                  <img src={logo4} alt="Logo 4" className="partner-logo" />
                  <img src={logo5} alt="Logo 5" className="partner-logo" />
                </div>
              </div>
              <div className="carousel-item">
                <div className="d-flex justify-content-center gap-4 flex-wrap">
                  <img src={logo1} alt="Logo 6" className="partner-logo" />
                  <img src={logo2} alt="Logo 7" className="partner-logo" />
                  <img src={logo3} alt="Logo 8" className="partner-logo" />
                  <img src={logo4} alt="Logo 9" className="partner-logo" />
                  <img src={logo5} alt="Logo 10" className="partner-logo" />
                </div>
              </div>
            </div>
            <button className="carousel-control-prev" type="button" data-bs-target="#partnerCarousel" data-bs-slide="prev">
              <span className="carousel-control-prev-icon rounded-circle"></span>
            </button>
            <button className="carousel-control-next" type="button" data-bs-target="#partnerCarousel" data-bs-slide="next">
              <span className="carousel-control-next-icon rounded-circle"></span>
            </button>
          </div>
        </div>
      </section>
    </>
  );
}

export default Home;