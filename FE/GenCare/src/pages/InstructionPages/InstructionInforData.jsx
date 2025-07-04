import InstructionInfor from "./InstructionInfor";

function InstructionInforData({ type }) {
  const contents = {
    payment: {
      title: "Hướng dẫn thanh toán",
      paragraphs: [
    "Quý khách có thể lựa chọn một trong các phương thức thanh toán phí xét nghiệm ADN sau đây:",
    "- **Chuyển khoản ngân hàng**: Vui lòng ghi rõ họ tên người xét nghiệm trong nội dung chuyển khoản để dễ dàng đối chiếu.",
    "- **Thanh toán trực tiếp**: Đến văn phòng công ty để thực hiện thanh toán trực tiếp với nhân viên tiếp nhận.",
    "- **Thanh toán tại nhà**: Áp dụng đối với khu vực nội thành, nhân viên thu mẫu sẽ thu phí trực tiếp khi đến thu mẫu.",
    "📌 **Lưu ý**: Quý khách vui lòng giữ lại biên lai hoặc ảnh chụp giao dịch để đối chiếu khi cần thiết.",
    "🔒 Mọi thông tin thanh toán được bảo mật tuyệt đối và chỉ sử dụng cho mục đích xác nhận dịch vụ."

      ],
    },
    testing: {
      title: "Thủ tục xét nghiệm ADN",
      paragraphs: [
        "Thủ tục xét nghiệm ADN bao gồm các bước sau:",
        "1. Đăng ký dịch vụ: Quý khách liên hệ hotline hoặc đăng ký qua website.",
        "2. Thu mẫu: Có thể đến văn phòng công ty, hoặc nhân viên đến thu mẫu tại nhà.",
        "3. Ký cam kết và xác nhận thông tin mẫu xét nghiệm.",
        "4. Tiến hành phân tích ADN tại phòng thí nghiệm đạt chuẩn quốc tế ISO 17025.",
        "5. Nhận kết quả: Qua email, bưu điện hoặc nhận trực tiếp tại văn phòng.",
        "Thời gian trả kết quả: từ 1 đến 7 ngày làm việc tùy theo gói dịch vụ đã chọn."
      ],
    },
    sample: {
      title: "Hướng dẫn thu mẫu",
      paragraphs: [
        "Để đảm bảo kết quả chính xác, việc thu mẫu cần tuân thủ hướng dẫn sau:",
        "1. Mẫu phổ biến nhất là tăm bông ngoáy miệng (niêm mạc miệng).",
        "2. Tránh ăn uống hoặc hút thuốc ít nhất 1 giờ trước khi lấy mẫu.",
        "3. Dùng tăm bông sạch, nhẹ nhàng chà sát mặt trong má khoảng 30 giây.",
        "4. Mỗi người cần ít nhất 2-3 que mẫu, để khô tự nhiên rồi cho vào túi giấy.",
        "5. Đánh dấu rõ tên, quan hệ, và ngày thu mẫu trên bao bì.",
        "Ngoài ra, có thể sử dụng mẫu máu khô, móng tay, tóc có chân tóc nếu cần."
      ],
    },
    prenatal: {
      title: "Hướng dẫn xét nghiệm thai nhi trước sinh",
      paragraphs: [
        "Xét nghiệm ADN thai nhi trước sinh giúp xác định quan hệ huyết thống mà không gây hại cho mẹ và bé.",
        "Có 2 phương pháp chính:",
        "1. Phương pháp không xâm lấn (NIPT): Lấy 10ml máu mẹ từ tuần thai thứ 9 trở đi.",
        "2. Phương pháp xâm lấn: Lấy mẫu nước ối hoặc nhau thai, yêu cầu cơ sở y tế hỗ trợ.",
        "Bố cần cung cấp mẫu tăm bông miệng hoặc mẫu máu khô.",
        "Thời gian trả kết quả: 7-10 ngày làm việc.",
        "Tính bảo mật và chính xác cao, được sử dụng trong các trường hợp xác định huyết thống sớm."
      ],
    },
    remains: {
      title: "Giám định ADN hài cốt liệt sỹ",
      paragraphs: [
        "Giám định ADN hài cốt liệt sỹ nhằm xác định danh tính và trả lại tên cho các anh hùng liệt sĩ.",
        "Các bước thực hiện:",
        "1. Thu thập mẫu hài cốt (xương, răng) bởi cơ quan chức năng hoặc thân nhân.",
        "2. Đồng thời lấy mẫu của người thân (cha mẹ, con, anh chị em ruột).",
        "3. Phân tích ADN tại phòng thí nghiệm đạt chuẩn quốc tế.",
        "4. So sánh kết quả để xác định mối quan hệ huyết thống.",
        "Quá trình có thể mất từ 20 đến 40 ngày do mẫu hài cốt cần được xử lý đặc biệt.",
        "Đây là dịch vụ mang ý nghĩa nhân văn và cần sự phối hợp chặt chẽ với cơ quan chức năng."
      ],
    },
    immigration: {
      title: "Thủ tục xét nghiệm ADN bảo lãnh di dân nước ngoài",
      paragraphs: [
        "Xét nghiệm ADN theo yêu cầu của lãnh sự quán phục vụ mục đích bảo lãnh định cư (Mỹ, Canada, Úc,...)",
        "Quy trình thực hiện:",
        "1. Đăng ký dịch vụ tại công ty và cung cấp giấy tờ cần thiết.",
        "2. Lấy mẫu theo yêu cầu của lãnh sự (đúng quy trình, có nhân viên chứng kiến, chụp hình, ký xác nhận).",
        "3. Gửi mẫu đến phòng thí nghiệm được lãnh sự công nhận.",
        "4. Nhận kết quả qua email và gửi trực tiếp đến lãnh sự quán.",
        "Thời gian xử lý: 7-14 ngày làm việc.",
        "Đảm bảo tuân thủ đúng quy định và hướng dẫn từ cơ quan di trú."
      ],
    },
  };

  const data = contents[type] || {
    title: "Không tìm thấy nội dung",
    paragraphs: ["Trang bạn yêu cầu hiện không tồn tại."],
  };

  return <InstructionInfor title={data.title} paragraphs={data.paragraphs} />;
}

export default InstructionInforData;
