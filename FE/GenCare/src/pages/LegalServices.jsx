import React,{ useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import{Link} from 'react-router-dom';

function LegalServices() {
  const { hash } = useLocation();
  
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

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">
                    <Link to="/legal-duration">
                    Xét Nghiệm Cha/Mẹ-Con
                    </Link>
                    </h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Anh/Chị-Em</h4>
                </div>
              </div>

          </div>
          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm họ hàng-Cháu</h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Ông/Bà-Cháu</h4>
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

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm hình sự</h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm truy vết thủ phạm</h4>
                </div>
              </div>

          </div>
          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm kiểm chứng tại tòa</h4>
                </div>
              </div>
          </div>

         </div>{/* row */}
       </div>{/* background */}
    </div>
  );
}

export default LegalServices;