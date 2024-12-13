IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'VehiclesDb')
    BEGIN
        CREATE DATABASE VehiclesDb;
        PRINT 'Database VehiclesDb created successfully.';
    END
ELSE
    BEGIN
        PRINT 'Database VehiclesDb already exists.';
    END