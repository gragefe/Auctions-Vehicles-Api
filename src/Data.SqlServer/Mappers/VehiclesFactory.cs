namespace Data.SqlServer.Mappers;

using Infrastructure.Crosscutting.Validations;
using SqlEntities = Data.SqlServer.Entities;
using DomainAbstract = Domain.Model.Abstract;
using DomainEnum = Domain.Model.Enum;

public static class VehiclesFactory
{
    public static DomainAbstract.Vehicle ToDomain(this SqlEntities.Vehicle dtoVehicle)
    {
        return dtoVehicle.Type switch
        {
            Data.SqlServer.Enum.VehicleType.HatchBack => HatchBackSqlMapper.ToDomain(dtoVehicle),
            Data.SqlServer.Enum.VehicleType.Sedan => SedanSqlMapper.ToDomain(dtoVehicle),
            Data.SqlServer.Enum.VehicleType.Suv => SuvSqlMapper.ToDomain(dtoVehicle),
            Data.SqlServer.Enum.VehicleType.Truck => TruckSqlMapper.ToDomain(dtoVehicle),
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }

    public static SqlEntities.Vehicle ToSql(this DomainAbstract.Vehicle domainVehicle)
    {
        return domainVehicle.Type switch
        {
            DomainEnum.VehicleType.HatchBack => MappToSqlVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Sedan => MappToSqlVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Suv => MappToSqlVehicle.ToDto(domainVehicle),
            DomainEnum.VehicleType.Truck => MappToSqlVehicle.ToDto(domainVehicle),
            _ => throw new CustomValidationException(CustomValidationMessages.NonExistentVehicleType)
        };
    }
}