namespace Application.Services.Mappers;

using DTO = Application.DTO;

public static class MappToDtoVehicle
{
    public static DTO.Vehicle ToDto(Domain.Model.Abstract.Vehicle domainVehicle)
    {

        if(domainVehicle == null)
        {
            return null;
        }

        return new DTO.Vehicle
        {
            Id = domainVehicle.Id,
            Type = domainVehicle.Type.ToDto(),
            UniqueIdentifier = domainVehicle.UniqueIdentifier,
            Manufacturer = domainVehicle.Manufacturer,
            Model = domainVehicle.Model,
            Year = domainVehicle.Year,
            StartingBid = domainVehicle.StartingBid,
            NumberOfDoors = domainVehicle.NumberOfDoors,
        };
    }
}