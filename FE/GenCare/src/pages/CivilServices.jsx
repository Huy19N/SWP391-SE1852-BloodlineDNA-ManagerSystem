import React from 'react';

function CivilServices() {
  return (
    <div className="container mt-5">
      <div className="text-center">
        <h1>Bảng giá dịch vụ dân sự</h1>
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 className="mx-4 text-primary text-center">BẢNG GIÁ DÂN SỰ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">
            {/* GÓI 24H */}
            <div className="col-md-6 mb-4">
                <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">GÓI 24H CÓ KẾT QUẢ</h4>
                </div>
                    <div className="card-body p-0">
                        <table className="table table-hover mb-0">
                        <tbody>
                        <tr>
                            <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                            2,500,000đ / 1 người
                            </td>
                        </tr>
                        <tr><td className="text-center"></td><td>Kết quả bảo mật tuyệt đối</td></tr>
                        <tr><td className="text-center"></td><td>Độ chính xác &gt; 99.9999%</td></tr>
                        <tr><td className="text-center"></td><td>Miễn phí xét nghiệm mở rộng, lần 2 nếu không huyết thống</td></tr>
                        <tr><td className="text-center"></td><td>Thu mẫu tại nhà MIỄN PHÍ</td></tr>
                        <tr><td className="text-center"></td><td>Không phụ thu thêm phí mẫu đặc biệt</td></tr>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>

            {/* GÓI 6H */}
            <div className="col-md-6 mb-4">
                <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">GÓI 6H CÓ KẾT QUẢ</h4>
                </div>
                    <div className="card-body p-0">
                        <table className="table table-hover mb-0">
                        <tbody>
                        <tr>
                            <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                            2,500,000đ / 1 người
                            </td>
                        </tr>
                        <tr><td className="text-center"></td><td>Kết quả bảo mật tuyệt đối</td></tr>
                        <tr><td className="text-center"></td><td>Độ chính xác &gt; 99.9999%</td></tr>
                        <tr><td className="text-center"></td><td>Miễn phí xét nghiệm mở rộng, lần 2 nếu không huyết thống</td></tr>
                        <tr><td className="text-center"></td><td>Thu mẫu tại nhà MIỄN PHÍ</td></tr>
                        <tr><td className="text-center"></td><td>Không phụ thu thêm phí mẫu đặc biệt</td></tr>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
            {/* GÓI 48H */}
            <div className="col-md-6 mb-4">
                <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">GÓI 48H CÓ KẾT QUẢ</h4>
                </div>
                    <div className="card-body p-0">
                        <table className="table table-hover mb-0">
                        <tbody>
                        <tr>
                            <td colSpan="2" className="text-center fw-bold fs-5 text-decoration-underline">
                            2,500,000đ / 1 người
                            </td>
                        </tr>
                        <tr><td className="text-center"></td><td>Kết quả bảo mật tuyệt đối</td></tr>
                        <tr><td className="text-center"></td><td>Độ chính xác &gt; 99.9999%</td></tr>
                        <tr><td className="text-center"></td><td>Miễn phí xét nghiệm mở rộng, lần 2 nếu không huyết thống</td></tr>
                        <tr><td className="text-center"></td><td>Thu mẫu tại nhà MIỄN PHÍ</td></tr>
                        <tr><td className="text-center"></td><td>Không phụ thu thêm phí mẫu đặc biệt</td></tr>
                        </tbody>
                    </table>
                    </div>
                </div>
            </div>
        </div>{/*  thẻ bản */}
            <div className="mt-5">
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
            </div>
      </div>{/*  thẻ khung */}
    </div>
  );
}

export default CivilServices;