import React,{ useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import{Link} from 'react-router-dom';
import{useNavigate} from 'react-router-dom';

function CivilServices() {
  const { hash } = useLocation();
  const navigate = useNavigate(); // Hook để điều hướng

  const handleClick = () => {
    navigate("/civil-duration"); // Chuyển trang 
  };

  useEffect(() => {
    if (hash) {
      const id = hash.replace("#", "");
      const element = document.getElementById(id);
      if (element) {

        setTimeout(() => {
          element.scrollIntoView({ behavior: "smooth", block: "center" });
        }, 100);
      }
    }
  }, [hash]);
  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>dịch vụ dân sự</h1>
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id= "Civil-Type-1" className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">
          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm Cha/Mẹ-Con</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm Anh/Chị-Em</h3>
                  </div>
                </div>
              </Link>
            </div>


          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm song sinh</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm Cô/Chú-Cháu</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm Dì/Cậu-Cháu</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm Ông/Bà-Cháu</h3>
                  </div>
                </div>
              </Link>
            </div>

        </div> {/* row */}
      </div> {/* background */}

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id="Civil-Type-2" className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 2</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm bệnh di truyền</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm gen đột biến</h3>
                  </div>
                </div>
              </Link>
            </div>
          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm sàng lọc trước sinh</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm di truyền ung thư</h3>
                  </div>
                </div>
              </Link>
            </div>
          {/* <div className="container mt-5">
          <button className="btn btn-primary mt-3" onClick={handleClick}>
              đăng ký ngay
          </button>
          </div> */}

        </div> {/* row */}
      </div>{/* background */}

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id="Civil-Type-3" className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 3</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm định danh cá nhân</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm động vật</h3>
                  </div>
                </div>
              </Link>
            </div>

          <div className="col-md-4 rounded-3">
              <Link to="/civil-duration" className="text-decoration-none text-dark">
                <div className="card h-100 shadow border-0 rounded-3">
                  <img src="/" className="card-img-top" alt="Xét nghiệm cha mẹ con" style={{ objectFit: "cover", height: "250px" }} />
                  <div className="card-body">
                    <h3>Xét nghiệm huyết thống thai nhi</h3>
                  </div>
                </div>
              </Link>
            </div>


        </div> {/* row */}
      </div>{/* background */}
      
    </div>
  );
}

export default CivilServices;