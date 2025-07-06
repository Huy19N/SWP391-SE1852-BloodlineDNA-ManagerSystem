# Base image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Environment variables để set SA password và chấp nhận license
ENV SA_PASSWORD=StrongPassword123
ENV ACCEPT_EULA=Y

# Copy script SQL hoặc backup file vào container
COPY init-db.sql /usr/src/app/init-db.sql

# Copy script shell để khởi tạo DB nếu muốn
COPY entrypoint.sh /usr/src/app/entrypoint.sh
RUN chmod +x /usr/src/app/entrypoint.sh

# Set entrypoint (nếu muốn chạy script tự động khi container start)
CMD ["/usr/src/app/entrypoint.sh"]