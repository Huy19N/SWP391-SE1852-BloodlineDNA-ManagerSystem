FROM mcr.microsoft.com/mssql/server:2022-latest

# Thiết lập biến môi trường
ENV ACCEPT_EULA=Y
# Khuyến nghị mạnh mật khẩu
ENV SA_PASSWORD=12345   

# Đặt thư mục làm việc
WORKDIR /usr/src/app

# Copy script và entrypoint
COPY GeneCareDataBase.sql .
COPY entrypoint.sh .

# Cấp quyền thực thi
RUN chmod +x entrypoint.sh

# Mở cổng mặc định
EXPOSE 1433

# Thiết lập entrypoint script (sẽ chạy khi container khởi động)
ENTRYPOINT ["/usr/src/app/entrypoint.sh"]
