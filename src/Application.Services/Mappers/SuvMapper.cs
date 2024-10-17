namespace Application.Services.Mappers;

using DomainEntities = Domain.Model.Entities;
public static class SuvMapper
{
    public static DomainEntities.Suv ToDomain(DTO.Vehicle dtoVehicle)
    {
        if (dtoVehicle == null)
        {
            return null;
        }

        return new DomainEntities.Suv
        {
            Id = dtoVehicle.Id,
            Type = dtoVehicle.Type.ToDomain(),
            UniqueIdentifier = dtoVehicle.UniqueIdentifier,
            Manufacturer = dtoVehicle.Manufacturer,
            Model = dtoVehicle.Model,
            Year = dtoVehicle.Year,
            StartingBid = dtoVehicle.StartingBid,
            NumberOfSeats = dtoVehicle.NumberOfSeats,
        };
    }
}