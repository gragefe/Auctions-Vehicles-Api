namespace Application.Services.Mappers;

using DomainEntities = Domain.Model.Entities;
public static class SedanMapper
{
    public static DomainEntities.Sedan ToDomain(DTO.Vehicle dtoVehicle)
    {
        if (dtoVehicle == null)
        {
            return null;
        }

        return new DomainEntities.Sedan
        {
            Id = dtoVehicle.Id,
            Type = dtoVehicle.Type.ToDomain(),
            UniqueIdentifier = dtoVehicle.UniqueIdentifier,
            Manufacturer = dtoVehicle.Manufacturer,
            Model = dtoVehicle.Model,
            Year = dtoVehicle.Year,
            StartingBid = dtoVehicle.StartingBid,
            NumberOfDoors = dtoVehicle.NumberOfDoors,
        };
    }
}