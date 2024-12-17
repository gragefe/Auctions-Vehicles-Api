USE VehiclesDb
GO

IF NOT EXISTS (
    SELECT 1 
    FROM INFORMATION_SCHEMA.TABLES 
    WHERE TABLE_NAME = N'Vehicles' AND TABLE_SCHEMA = 'dbo'
)
BEGIN
CREATE TABLE Vehicles (
                          Id UNIQUEIDENTIFIER PRIMARY KEY,
                          Type INTEGER,
                          UniqueIdentifier NVARCHAR(255) NOT NULL,
                          Manufacturer NVARCHAR(255) NOT NULL,
                          Model NVARCHAR(255) NOT NULL,
                          Year INTEGER,
                          StartingBid FLOAT NOT NULL,
                          NumberOfDoors INTEGER,
                          NumberOfSeats INTEGER,
                          LoadCapacity INTEGER
);
PRINT 'Table Vehicles created successfully.';
END
ELSE
BEGIN
    PRINT 'Table Vehicles already exists.';
END
