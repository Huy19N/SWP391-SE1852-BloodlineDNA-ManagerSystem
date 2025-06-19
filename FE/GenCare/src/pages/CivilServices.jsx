import { Button } from 'bootstrap';
import React,{ useEffect } from 'react';
import { Link,useLocation,useNavigate } from 'react-router-dom';


function CivilServices() {
  
  const { hash } = useLocation();
  const navigate = useNavigate();

  const handleSelect =(type, subType, testType)=>{
    localStorage.setItem('selectedService',JSON.stringify({
      mainType: type,//loại dịch vụ
      subType: subType,//kiểu xét nghiệm
      testType: testType//loại xét nghiệm
    }));
    navigate("/civil-duration"); 
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
        {/* {selectedService && (
          <p className="fs-4">
            Bạn đã chọn <strong>{selectedService.mainType}</strong> - <strong>{selectedService.subType}</strong>
          </p>
        )} */}
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id= "Civil-Type-1" className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">
          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','Cha/Mẹ-Con')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Cha/Mẹ-Con</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','Anh/Chị-Em')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Anh/Chị-Em" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Anh/Chị-Em</h3>
              </div>
            </div>
          </div>


          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','song sinh')}>
              <img src="" className="card-img-top" alt="Xét nghiệm song sinh" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm song sinh</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','Cô/Chú-Cháu')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cô/Chú-Cháu" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Cô/Chú-Cháu</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','Dì/Cậu-Cháu')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Dì/Cậu-Cháu" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Dì/Cậu-Cháu</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 1','Ông/Bà-Cháu')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Ông/Bà-Cháu" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Ông/Bà-Cháu</h3>
              </div>
            </div>
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
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 2','bệnh di truyền')}>
              <img src="" className="card-img-top" alt="Xét nghiệm bệnh di truyền" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm bệnh di truyền</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 2','gen đột biến')}>
              <img src="" className="card-img-top" alt="Xét nghiệm gen đột biến" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm gen đột biến</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 2','sàng lọc trước sinh')}>
              <img src="" className="card-img-top" alt="Xét nghiệm sàng lọc trước sinh" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm sàng lọc trước sinh</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 2','di truyền ung thư')}>
              <img src="" className="card-img-top" alt="Xét nghiệm di truyền ung thư" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm di truyền ung thư</h3>
              </div>
            </div>
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
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 3','định danh cá nhân')}>
              <img src="" className="card-img-top" alt="Xét nghiệm định danh cá nhân" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm định danh cá nhân</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 3','động vật')}>
              <img src="" className="card-img-top" alt="Xét nghiệm động vật" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm động vật</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('dân sự','loại 3','huyết thống thai nhi')}>
              <img src="" className="card-img-top" alt="Xét nghiệm huyết thống thai nhi" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm huyết thống thai nhi</h3>
              </div>
            </div>
          </div>

        </div> {/* row */}
      </div>{/* background */}
      
    </div>
  );
}

export default CivilServices;