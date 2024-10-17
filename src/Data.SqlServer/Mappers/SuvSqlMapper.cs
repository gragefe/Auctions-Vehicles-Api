namespace Data.SqlServer.Mappers;
using DomainEntities = Domain.Model.Entities;
using SqlEntities = Data.SqlServer.Entities;
public  class SuvSqlMapper
{
    public static DomainEntities.Suv ToDomain(SqlEntities.Vehicle sqlVehicle)
    {
        if (sqlVehicle == null)
        {
            return null;
        }

        return new DomainEntities.Suv
        {
            Id = sqlVehicle.Id,
            Type = sqlVehicle.Type.ToDomain(),
            UniqueIdentifier = sqlVehicle.UniqueIdentifier,
            Manufacturer = sqlVehicle.Manufacturer,
            Model = sqlVehicle.Model,
            Year = sqlVehicle.Year,
            StartingBid = sqlVehicle.StartingBid,
            NumberOfSeats = sqlVehicle.NumberOfSeats,
        };
    }
}
