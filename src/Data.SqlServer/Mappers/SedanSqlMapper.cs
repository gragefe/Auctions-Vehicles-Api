namespace Data.SqlServer.Mappers;
using DomainEntities = Domain.Model.Entities;
using SqlEntities = Data.SqlServer.Entities;
public  class SedanSqlMapper
{
    public static DomainEntities.Sedan ToDomain(SqlEntities.Vehicle sqlVehicle)
    {
        if (sqlVehicle == null)
        {
            return null;
        }

        return new DomainEntities.Sedan
        {
            Id = sqlVehicle.Id,
            Type = sqlVehicle.Type.ToDomain(),
            UniqueIdentifier = sqlVehicle.UniqueIdentifier,
            Manufacturer = sqlVehicle.Manufacturer,
            Model = sqlVehicle.Model,
            Year = sqlVehicle.Year,
            StartingBid = sqlVehicle.StartingBid,
            NumberOfDoors = sqlVehicle.NumberOfDoors,
        };
    }
}
