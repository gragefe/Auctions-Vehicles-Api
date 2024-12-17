namespace Application.Services.Mappers;

using Infrastructure.Crosscutting.Validations;
using DomainAbstract = Domain.Model.Abstract;
using DomainEnum = Domain.Model.Enum;
using DTO = DTO;

public static class VehiclesFactory
{
    public static DomainAbstract.Vehicle ToDomain(this DTO.Vehicle dtoVehicle)
    {
        return dtoVehicle.Type switch
        {
            DTO.Enum.VehicleType.HatchBack => HatchBackMapper.ToDomain(dtoVehicle),
            DTO.Enum.VehicleType.Sedan => SedanMapper.ToDomain(dtoVehicle),
            DTO.Enum.VehicleType.Suv => SuvMapper.ToDomain(dtoVehicle),
            DTO.Enum.VehicleType.Truck => TruckMapper.ToDomain(dtoVehicle),
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }

    public static DTO.Vehicle ToDto(this DomainAbstract.Vehicle domainVehicle)
    {
        return domainVehicle.Type switch
        {
            DomainEnum.VehicleType.HatchBack => MappToDtoVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Sedan => MappToDtoVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Suv => MappToDtoVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Truck => MappToDtoVehicle.ToDto(domainVehicle),
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }
}