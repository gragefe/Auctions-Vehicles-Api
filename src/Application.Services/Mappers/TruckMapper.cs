namespace Application.Services.Mappers;

using DomainEntities = Domain.Model.Entities;
public static class TruckMapper
{
    public static DomainEntities.Truck ToDomain(DTO.Vehicle dtoVehicle)
    {
        if (dtoVehicle == null)
        {
            return null;
        }

        return new DomainEntities.Truck
        {
            Id = dtoVehicle.Id,
            Type = dtoVehicle.Type.ToDomain(),
            UniqueIdentifier = dtoVehicle.UniqueIdentifier,
            Manufacturer = dtoVehicle.Manufacturer,
            Model = dtoVehicle.Model,
            Year = dtoVehicle.Year,
            StartingBid = dtoVehicle.StartingBid,
            LoadCapacity = dtoVehicle.LoadCapacity,
        };
    }
}