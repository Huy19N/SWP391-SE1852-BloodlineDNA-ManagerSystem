
function Instruction(){
    return(
        <>        
            <div className="max-w-4xl mx-auto p-6 mt-[56px] text-gray-800">
            <h1 className="text-3xl font-bold mb-6">🧬 Hướng Dẫn Đăng Ký & Thanh Toán Dịch Vụ Xét Nghiệm ADN</h1>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">🔍 Bước 1: Chọn Dịch Vụ Xét Nghiệm</h2>
                <ul className="list-disc pl-6 space-y-1">
                <li>Truy cập trang <strong>Dịch vụ xét nghiệm</strong> hoặc Trang chủ.</li>
                <li>Chọn loại xét nghiệm phù hợp (ví dụ: huyết thống, hành chính...).</li>
                <li>Xem mô tả, thời gian trả kết quả, chi phí.</li>
                </ul>
            </section>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">📝 Bước 2: Đăng Ký Dịch Vụ</h2>
                <ul className="list-disc pl-6 space-y-1">
                <li>Nhấn vào nút <strong>"Đăng ký xét nghiệm"</strong>.</li>
                <li>Điền đầy đủ thông tin cá nhân và chọn phương thức thu mẫu.</li>
                <li>Chọn ngày & giờ đặt hẹn nếu cần hỗ trợ.</li>
                </ul>
            </section>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">💳 Bước 3: Thanh Toán Dịch Vụ</h2>
                <ul className="list-disc pl-6 space-y-1">
                <li>Chọn phương thức thanh toán phù hợp:</li>
                <ul className="list-disc pl-6">
                    <li>Online: Momo, ZaloPay, ngân hàng, thẻ tín dụng</li>
                    <li>COD: Nhận bộ kit và thanh toán khi giao</li>
                    <li>Trực tiếp tại cơ sở y tế</li>
                </ul>
                <li>Hệ thống sẽ gửi hóa đơn và xác nhận sau khi thanh toán.</li>
                </ul>
            </section>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">📦 Bước 4: Nhận & Gửi Mẫu Xét Nghiệm</h2>
                <p className="mb-2 font-medium">A. Tự thu mẫu tại nhà:</p>
                <ul className="list-disc pl-6 mb-4">
                <li>Nhận bộ kit tại nhà</li>
                <li>Thu mẫu theo hướng dẫn</li>
                <li>Gửi lại mẫu đến cơ sở xét nghiệm</li>
                </ul>
                <p className="mb-2 font-medium">B. Thu mẫu tại cơ sở y tế:</p>
                <ul className="list-disc pl-6">
                <li>Đến đúng giờ hẹn</li>
                <li>Nhân viên y tế thu mẫu và xử lý thủ tục</li>
                </ul>
            </section>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">📄 Bước 5: Theo Dõi & Nhận Kết Quả</h2>
                <ul className="list-disc pl-6 space-y-1">
                <li>Đăng nhập tài khoản để theo dõi tiến trình.</li>
                <li>Nhận kết quả qua hệ thống, email hoặc bản cứng (nếu yêu cầu).</li>
                </ul>
            </section>

            <section className="mb-8">
                <h2 className="text-xl font-semibold mb-2">⭐ Phản Hồi & Hỗ Trợ</h2>
                <ul className="list-disc pl-6 space-y-1">
                <li>Gửi đánh giá dịch vụ sau khi nhận kết quả.</li>
                <li>Liên hệ qua hotline, email hoặc Zalo/Facebook nếu cần hỗ trợ.</li>
                </ul>
            </section>

            <div className="bg-gray-100 p-4 rounded-xl border text-sm">
                <strong>Lưu ý bảo mật:</strong> Thông tin cá nhân và kết quả xét nghiệm được bảo mật tuyệt đối theo quy định pháp luật và chính sách riêng tư.
            </div>
            </div>
        </>
    );
}

export default Instruction