 # Sử dụng image chính thức của Microsoft SQL Server 2022 cho Linux
FROM mcr.microsoft.com/mssql/server:2022-latest

# Thiết lập biến môi trường
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=12345

# Mở port SQL Server
EXPOSE 1433

# Nếu muốn khởi tạo dữ liệu, copy file .sql vào đây và dùng entrypoint custom
# COPY init-db.sql /docker-entrypoint-initdb.d/