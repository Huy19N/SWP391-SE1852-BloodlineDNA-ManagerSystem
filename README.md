# Tên của hệ thống
### Tên của hệ thống:  Bloodline DNA Testing Service Management System.

# Thành Viên:
- Nguyễn Gia Huy
- Huỳnh Trung Kiên
- Nguyễn Văn Thành Đạt
- Trần Nguyễn Hiếu Thuận

# Mục đích hệ thống là gì?
### Mục đích: Quản lý dịch vụ xét nghiệm ADN huyết thống của những anh chàng không hi vọng.
- Phần mềm quản lý dịch vụ xét nghiệm ADN huyết thống của 01 cơ sở y tế.

# Ai là người sử dụng?
### Ai:
- Guest
- Customer
- Staff
- Manager
- Admin

# Danh sách tính năng của hệ thống
### Tính năng:
 - Trang chủ giới thiệu cơ sở y tế, dịch vụ xét nghiệm ADN (dân sự, hành chính), blog chia sẽ kinh nghiệm kiến thức ADN & hướng dẫn xét nghiệm, ….
 - Chức năng cho phép người dùng đặt dịch vụ xét nghiệm bằng cách tự thu mẫu tại nhà hoặc yêu cầu cơ sở tự thu mẫu (tại nhà hoặc tại cơ sở y tế) tùy theo từng dịch vụ xét nghiệm ADN.
 - Quản lý quá trình thực hiện xét nghiệm bằmg cách tự gửi mẫu. (chỉ áp dụng cho các dịch vụ xét nghiệm ADN dân sự)
  << Đăng ký đặt hẹn dịch vụ xét nghiệm --> Nhận bộ kit xét nghiệm --> Thu thập mẫu xét nghiệm --> Chuyển mẫu đến cơ sở y tế --> Thực hiện xét nghiệm tại cơ sở y tế và ghi nhận kết quả --> Trả kết quả xét nghiệm >>
 - Quản lý quá trình thực hiện xét nghiệm tại cơ sở y tế
  << Đăng ký đặt hẹn dịch vụ xét nghiệm --> Nhân viên cơ sở y tế thu thập mẫu và cập nhật đơn yêu cầu xét nghiệm --> Thực hiện xét nghiệm tại cơ sở y tế và ghi nhận kết quả --> Trả kết quả xét nghiệm >>
 - Chức năng cho phép người dùng xem kết quả xét nghiệm trên hệ thống
 - Khai báo thông tin dịch vụ xét nghiệm, bảng giá, ....
 - Quản lý rating, feedback.
 - Quản lý hồ sơ người dùng, lịch sử đặt xét nghiệm.
 - Dashboard & Report.

# build Docker docker-compose.yml
docker-compose build
docker-compose logs -f 
docker-compose down

# Chạy hệ thống bằng Docker
### Yêu cầu:
- Docker và Docker Compose đã được cài đặt trên máy.
- Dự án sử dụng .NET 8.0 cho backend (APIGeneCare, GeneCare) và Node.js 22.13.1 cho frontend (FE/GenCare).
- Dịch vụ database sử dụng PostgreSQL (image: postgres:latest).

### Biến môi trường:
- Dịch vụ FE/GenCare sử dụng file `.env` (xem `FE/GenCare/.env`).
- Các dịch vụ backend có thể sử dụng file `.env` nếu cần (mặc định chưa bật trong docker-compose.yml).
- PostgreSQL sử dụng các biến mặc định:
  - `POSTGRES_USER=genecare`
  - `POSTGRES_PASSWORD=genecare`
  - `POSTGRES_DB=genecare`

### Cổng dịch vụ:
- `csharp-apigenecare` (API): http://localhost:8080
- `csharp-genecare` (Web): http://localhost:8081
- `js-gencare` (Frontend): http://localhost:3000
- `postgres-db`: không xuất cổng ra ngoài mặc định (bật nếu cần trong docker-compose.yml)

### Cách build và chạy hệ thống:
```bash
docker-compose build
docker-compose up -dn```
- Để dừng hệ thống:
```bash
docker-compose down
```

### Lưu ý cấu hình:
- Nếu cần sửa đổi cấu hình kết nối database, hãy cập nhật các file `appsettings.json` trong backend hoặc biến môi trường tương ứng.
- Đảm bảo file `.env` cho FE/GenCare đã được cấu hình đúng trước khi build.
- Các dịch vụ backend và frontend đã được cấu hình để chạy với user không phải root nhằm tăng bảo mật.
- Các port có thể thay đổi trong `docker-compose.yml` nếu cần thiết cho môi trường phát triển.

### Tham khảo thêm:
- Các Dockerfile và docker-compose.yml đã được tối ưu cho build cache và bảo mật.
- Nếu cần truy cập database từ bên ngoài, hãy mở port 5432 trong phần `postgres-db` của docker-compose.yml.
