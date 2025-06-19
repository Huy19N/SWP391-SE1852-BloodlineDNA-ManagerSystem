import React,{ useEffect } from 'react';
import {Link, useLocation,useNavigate } from 'react-router-dom';

function LegalServices() {
  const selectedService = JSON.parse(localStorage.getItem('selectedService'));
  const { hash } = useLocation();
  const navigate = useNavigate();
  
    const handleSelect =(type, subType, testType)=>{
      localStorage.setItem('selectedService',JSON.stringify({
        mainType: type,//loại dịch vụ
        subType: subType,//kiểu xét nghiệm
        testType: testType//loại xét nghiệm
      }));
      navigate("/legal-duration"); 
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
        <h1>dịch vụ pháp lí</h1>
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id="Legal-Type-1" className="mx-4 text-primary text-center">CÁC DỊCH VỤ PHÁP LÍ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 1','Cha/Mẹ-Con')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Cha/Mẹ-Con</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 1','Anh/Chị-Em')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Anh/Chị-Em</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 1','họ hàng-Cháu')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm họ hàng-Cháu</h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 1','Ông/Bà-Cháu')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm Ông/Bà-Cháu</h3>
              </div>
            </div>
          </div>

        </div> {/* row */}
      </div>{/*  background */}
          

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id="Legal-Type-2" className="mx-4 text-primary text-center">CÁC DỊCH VỤ PHÁP LÍ LOẠI 2</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 2','hình sự')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm hình sự </h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 2','truy vết tội phạm')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm truy vết tội phạm </h3>
              </div>
            </div>
          </div>

          <div className="col-md-4 rounded-3">
            <div
              className="card h-100 shadow border-0 rounded-3 text-dark text-decoration-none"
              onClick={() => handleSelect('Pháp lí','loại 2','kiểm chứng tại tòa')}>
              <img src="" className="card-img-top" alt="Xét nghiệm Cha/Mẹ-Con" style={{ objectFit: "cover", height: "250px" }} />
              <div className="card-body">
                <h3>Xét nghiệm kiểm chứng tại tòa </h3>
              </div>
            </div>
          </div>

         </div>{/* row */}
       </div>{/* background */}
    </div>
  );
}

export default LegalServices;