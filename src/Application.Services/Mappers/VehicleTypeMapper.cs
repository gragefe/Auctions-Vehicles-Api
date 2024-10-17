namespace Application.Services.Mappers;

using Infrastructure.Crosscutting.Validations;
using DomainEnum = Domain.Model.Enum;

public static class VehicleTypeMapper
{
    public static DomainEnum.VehicleType ToDomain(this DTO.Enum.VehicleType dtoVehicleType)
    {
        return dtoVehicleType switch
        {
            DTO.Enum.VehicleType.HatchBack => DomainEnum.VehicleType.HatchBack,
            DTO.Enum.VehicleType.Sedan => DomainEnum.VehicleType.Sedan,
            DTO.Enum.VehicleType.SUV => DomainEnum.VehicleType.SUV,
            DTO.Enum.VehicleType.Truck => DomainEnum.VehicleType.Truck,
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }

    public static DTO.Enum.VehicleType ToDto(this DomainEnum.VehicleType domainVehicleType)
    {
        return domainVehicleType switch
        {
            DomainEnum.VehicleType.HatchBack => DTO.Enum.VehicleType.HatchBack,
            DomainEnum.VehicleType.Sedan => DTO.Enum.VehicleType.Sedan,
            DomainEnum.VehicleType.SUV => DTO.Enum.VehicleType.SUV,
            DomainEnum.VehicleType.Truck => DTO.Enum.VehicleType.Truck,
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }
}