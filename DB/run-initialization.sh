#!/bin/bash
set -e

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait to be sure that SQL Server came up
sleep 90s

# Run the setup script to create the DB in the DB
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "v!ctorY8080" -d master -i create-database.sql
