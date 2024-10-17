namespace Application.Services.Mappers;

using DomainEntities = Domain.Model.Entities;
public static class HatchBackMapper
{
    public static DomainEntities.HatchBack ToDomain(DTO.Vehicle dtoVehicle)
    {
        if (dtoVehicle == null)
        {
            return null;
        }

        return new DomainEntities.HatchBack
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