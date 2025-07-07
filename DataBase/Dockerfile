FROM mcr.microsoft.com/mssql/server:2022-latest

WORKDIR /app

ENV SA_PASSWORD=12345
ENV ACCEPT_EULA=Y

COPY GeneCareDataBase.sql ./bd/GeneCareDataBase.sql
COPY exec.sh ./db/exec.sh

EXPOSE 1433

CMD ["./db/exec.sh"]
