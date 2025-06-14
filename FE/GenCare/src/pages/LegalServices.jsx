import React,{ useEffect } from 'react';
import { useLocation } from 'react-router-dom';

function LegalServices() {
  const { hash } = useLocation();

  useEffect(() => {
    if (hash) {
      const element = document.getElementById(hash.replace("#", ""));
      if (element) {
        element.scrollIntoView({ behavior: "smooth" });
      }
    }
  }, [hash]);

  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>dịch vụ pháp lý</h1>
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 id="" className="mx-4 text-primary text-center">CÁC DỊCH VỤ PHÁP LÍ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Cha/Mẹ-Con</h4>
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

        </div> {/* thẻ bảng */}
        
        {/* <div className="mt-5">
            <h5>- Pháp lý loại 1 xác minh chứng cứ có giá trị pháp lý liên quan tới người thân như:</h5>
            <ul className="list-unstyled">
                <li>+ Tranh chấp tài sản, di chúc, di sản thừa kế</li>
                <li>+ Tranh chấp về quyền nuôi con, cấp dưỡng</li>
                <li>+ Thủ tục ly hôn có yêu cầu giám định con</li>
                <li>+ Xác minh danh tính người thân mất tích</li>
                <li>+ Hỗ trợ xác minh nhân thân trong các vụ mạo danh, tráo đổi, lừa đảo</li>
            </ul>
            <h5>- Thời gian trả kết quả không bao gồm ngày thu mẫu, Thứ 7, Chủ Nhật và các ngày Lễ.</h5>
            <h5>- Kết quả ADN Pháp lý thì nhân viên cơ sở và người có thẩm quyền sẽ trực tiếp thu mẫu.</h5>
            <h5>- Xét nghiệm ADN lại lần 2 hoàn toàn miễn phí nếu kết quả kiểm tra lần thứ nhất KHÔNG cùng huyết thống để Hội đồng khoa học đánh giá và đưa ra kết luận tuyệt đối chính xác.</h5>
            <h5>- Không phụ thu (đã tính giá các loại thủ tục 500,000/lần và phí dịch vụ 500,000đ/lần)</h5>
        </div> */}
      </div>{/*  thẻ khung */}
      
    </div>
  );
}

export default LegalServices;