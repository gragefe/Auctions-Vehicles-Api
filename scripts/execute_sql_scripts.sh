#!/bin/bash

echo "Waiting for SQL Server to be available..."

# Maximum wait time (in seconds)
TIMEOUT=60
RETRY_INTERVAL=5
ELAPSED=0

# Loop to check SQL Server availability
while ! /opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P VehicleSqlPAssword123 -Q "SELECT 1;" > /dev/null 2>&1; do
  if [ $ELAPSED -ge $TIMEOUT ]; then
    echo "Timeout reached. SQL Server is not accessible."
    exit 1
  fi
  echo "SQL Server is not ready yet. Retrying in $RETRY_INTERVAL seconds..."
  sleep $RETRY_INTERVAL
  ELAPSED=$((ELAPSED + RETRY_INTERVAL))
done

echo "SQL Server is available. Executing scripts..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P VehicleSqlPAssword123 -d master -i /tmp/create_vehicles_db.sql
/opt/mssql-tools/bin/sqlcmd -S sqlserver -U sa -P VehicleSqlPAssword123 -d master -i /tmp/create_vehicles_table.sql
