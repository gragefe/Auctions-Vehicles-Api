namespace Unit.Tests.Application.Mappers;

using global::Application.DTO;
using global::Application.DTO.Enum;
using global::Application.Services.Mappers;

public class VehiclesFactoryMapperTests
{
    [Fact]
    public void ToDomain_WithHatchBackDto_ReturnsHatchBackDomain()
    {
        // Arrange
        var dtoVehicle = new Vehicle { Type = VehicleType.HatchBack };

        // Act
        var domainVehicle = VehiclesFactory.ToDomain(dtoVehicle);

        // Assert
        Assert.IsType<global::Domain.Model.Entities.HatchBack>(domainVehicle);
    }

    [Fact]
    public void ToDomain_WithSedanDto_ReturnsSedanDomain()
    {
        // Arrange
        var dtoVehicle = new Vehicle { Type = VehicleType.Sedan };

        // Act
        var domainVehicle = VehiclesFactory.ToDomain(dtoVehicle);

        // Assert
        Assert.IsType<global::Domain.Model.Entities.Sedan>(domainVehicle);
    }

    [Fact]
    public void ToDomain_WithSuvDto_ReturnsSuvDomain()
    {
        // Arrange
        var dtoVehicle = new Vehicle { Type = VehicleType.SUV };

        // Act
        var domainVehicle = VehiclesFactory.ToDomain(dtoVehicle);

        // Assert
        Assert.IsType<global::Domain.Model.Entities.Suv>(domainVehicle);
    }

    [Fact]
    public void ToDomain_WithTruckDto_ReturnsTruckDomain()
    {
        // Arrange
        var dtoVehicle = new Vehicle { Type = VehicleType.Truck };

        // Act
        var domainVehicle = VehiclesFactory.ToDomain(dtoVehicle);

        // Assert
        Assert.IsType<global::Domain.Model.Entities.Truck>(domainVehicle);
    }
}