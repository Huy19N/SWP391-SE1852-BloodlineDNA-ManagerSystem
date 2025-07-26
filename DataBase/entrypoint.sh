#!/bin/bash
/opt/mssql/bin/sqlservr &

echo "Waiting for SQL Server to start..."
sleep 20

echo "Running init script..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /usr/src/app/GeneCareDataBase.sql

wait
