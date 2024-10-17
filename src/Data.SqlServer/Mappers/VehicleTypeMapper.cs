namespace Data.SqlServer.Mappers;

using DomainEnum = Domain.Model.Enum;
using SqlEnum = Data.SqlServer.Enum;
using Infrastructure.Crosscutting.Validations;

public static class VehicleTypeMapper
{
    public static DomainEnum.VehicleType ToDomain(this SqlEnum.VehicleType dtoVehicleType)
    {
        return dtoVehicleType switch
        {
            SqlEnum.VehicleType.HatchBack => DomainEnum.VehicleType.HatchBack,
            SqlEnum.VehicleType.Sedan => DomainEnum.VehicleType.Sedan,
            SqlEnum.VehicleType.SUV => DomainEnum.VehicleType.SUV,
            SqlEnum.VehicleType.Truck => DomainEnum.VehicleType.Truck,
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }

    public static SqlEnum.VehicleType ToDto(this DomainEnum.VehicleType domainVehicleType)
    {
        return domainVehicleType switch
        {
            DomainEnum.VehicleType.HatchBack => SqlEnum.VehicleType.HatchBack,
            DomainEnum.VehicleType.Sedan => SqlEnum.VehicleType.Sedan,
            DomainEnum.VehicleType.SUV => SqlEnum.VehicleType.SUV,
            DomainEnum.VehicleType.Truck => SqlEnum.VehicleType.Truck,
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }
}
