FROM mcr.microsoft.com/mssql/server:2022-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=ABC@abc123

WORKDIR /usr/src/app

COPY GeneCareDataBase.sql .
COPY entrypoint.sh .



USER root
RUN apt-get update && apt-get install -y dos2unix && dos2unix entrypoint.sh
RUN chmod +x entrypoint.sh
USER mssql

EXPOSE 1433

ENTRYPOINT ["/usr/src/app/entrypoint.sh"]