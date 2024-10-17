namespace Data.SqlServer.Mappers;
using DomainAbstract = Domain.Model.Abstract;
using SqlEntities = Data.SqlServer.Entities;

public static class MappToSqlVehicle
{
    public static SqlEntities.Vehicle ToDto(DomainAbstract.Vehicle domainVehicle)
    {

        if (domainVehicle == null)
        {
            return null;
        }

        return new SqlEntities.Vehicle
        {
            Id = domainVehicle.Id,
            Type = domainVehicle.Type.ToDto(),
            UniqueIdentifier = domainVehicle.UniqueIdentifier,
            Manufacturer = domainVehicle.Manufacturer,
            Model = domainVehicle.Model,
            Year = domainVehicle.Year,
            StartingBid = domainVehicle.StartingBid,
            NumberOfDoors = domainVehicle.NumberOfDoors
        };
    }
}
