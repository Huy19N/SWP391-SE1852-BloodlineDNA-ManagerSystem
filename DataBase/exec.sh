#!/bin/bash
# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."
sleep 20s

# Run script SQL
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "StrongPassword123" -d master -i /usr/src/app/init-db.sql

# Wait for SQL Server to finish
wait
echo "SQL Server is running and the script has been executed."