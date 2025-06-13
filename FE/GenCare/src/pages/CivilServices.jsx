import React from 'react';

function CivilServices() {
  return (
    <div className="container mt-5" style={{ paddingTop: '2rem' }}>
      <div className="text-center">
        <h1>dịch vụ dân sự</h1>
      </div>
      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 1</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">
          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Cha/Mẹ-Con</h4>
                </div>
              </div>
          </div>

          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Anh/Chị-Em</h4>
                </div>
              </div>

          </div>
          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm song sinh</h4>
                </div>
              </div>
          </div>

          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Cô/chú-Cháu</h4>
                </div>
              </div>
          </div>

          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Dì/Cậu-Cháu</h4>
                </div>
              </div>
          </div>

          <div className="col-md-4 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm Ông/Bà-Cháu</h4>
                </div>
              </div>
          </div>

          

        </div> {/* thẻ bảng */}




        {/* <div className="row">
            gói 24h
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

              gói 6h
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

            gói 48h
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
        </div> thẻ row */}
            {/* <div className="mt-5">
                <h5>- Dân sự loại 1 dùng để đăng ký, làm thủ tục như:</h5>
                <ul className="list-unstyled">
                    <li>+ Đăng ký nhận cha/mẹ-con.</li>
                    <li>+ Đăng ký khai sinh/khai tử.</li>
                    <li>+ Đăng ký kết hôn/li hôn.</li>
                    <li>+ Đăng ký hộ khẩu.</li>
                    <li>+ Đăng ký chứng minh nhân dân.</li>
                </ul>
                <h5>- Thời gian trả kết quả không bao gồm ngày thu mẫu, Thứ 7, Chủ Nhật và các ngày Lễ</h5>
                <h5>- Kết quả ADN Pháp lý thì nhân viên sẽ trực tiếp thu mẫu.</h5>
                <h5>- Nếu bạn có nhu cầu thu mẫu tại nhà vui lòng liên hệ qua Zalo/Hotline.</h5>
                <h5>- Xét nghiệm ADN lại lần 2 hoàn toàn miễn phí nếu kết quả kiểm tra lần thứ nhất KHÔNG cùng huyết thống.</h5>
            </div> */}
      </div>{/*  thẻ khung */}

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 2</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm bệnh di truyền</h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm gen đột biến</h4>
                </div>
              </div>

          </div>
          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm sàng lọc trước sinh</h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét Nghiệm di truyền ung thư</h4>
                </div>
              </div>
          </div>

        </div> {/* thẻ bảng */}
      </div>{/*  thẻ khung */}

      <div className="container mt-5 p-4 rounded shadow" style={{ background: 'rgba(255, 255, 255, 0.9)' }}>
        <div className="d-flex align-items-center mb-5">
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
          <h2 className="mx-4 text-primary text-center">CÁC DỊCH VỤ DÂN SỰ LOẠI 3</h2>
          <div className="flex-grow-1 border-top border-primary" style={{ height: '1px' }}></div>
        </div>
        <div className="row">

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm định danh cá nhân</h4>
                </div>
              </div>
          </div>

          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm động vật</h4>
                </div>
              </div>

          </div>
          <div className="col-md-6 mb-4">
            <div className="card shadow">
                <div className="card-header bg-info text-white text-center">
                  <h4 className="mb-0">Xét nghiệm huyết thống thai nhi</h4>
                </div>
              </div>
          </div>


        </div> {/* thẻ bảng */}
      </div>{/*  thẻ khung */}
      
    </div>
  );
}

export default CivilServices;