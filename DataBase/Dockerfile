FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=12345

WORKDIR /usr/src/app

COPY GeneCareDataBase.sql .
COPY entrypoint.sh .

USER root
RUN chmod +x entrypoint.sh
USER mssql

EXPOSE 1433

ENTRYPOINT ["/usr/src/app/entrypoint.sh"]