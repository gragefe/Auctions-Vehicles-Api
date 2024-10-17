namespace Unit.Tests.Application.Mappers;

using global::Application.DTO;
using global::Application.DTO.Enum;
using global::Application.Services.Mappers;
using Infrastructure.Crosscutting.Utils;

public class TruckMapperTests
{
    [Fact]
    public void ToDomain_NullVehicle_ReturnsNull()
    {
        // Act
        var result = TruckMapper.ToDomain(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ToDomain_ValidVehicle_ReturnsDomainVehicle()
    {
        // Arrange
        var truck = new Vehicle
        {
            Id = Guid.NewGuid(),
            Type = VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            NumberOfSeats = RandomValues.GetRandomNumber(5),
        };

        // Act
        var result = TruckMapper.ToDomain(truck);

        // Assert
        Assert.Equal(truck.Id, result.Id);
        Assert.Equal(truck.Type, result.Type.ToDto());
        Assert.Equal(truck.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(truck.Manufacturer, result.Manufacturer);
        Assert.Equal(truck.Model, result.Model);
        Assert.Equal(truck.Year, result.Year);
        Assert.Equal(truck.StartingBid, result.StartingBid);
    }

    [Fact]
    public void ToDto_NullVehicle_ReturnsNull()
    {
        // Act
        var result = MappToDtoVehicle.ToDto(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ToDto_ValidVehicle_ReturnsDtoVehicle()
    {
        // Arrange
        var truck = new global::Domain.Model.Entities.Truck
        {
            Id = Guid.NewGuid(),
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            LoadCapacity = RandomValues.GetRandomNumber(5),
        };

        // Act
        var result = MappToDtoVehicle.ToDto(truck);

        // Assert
        Assert.Equal(truck.Id, result.Id);
        Assert.Equal(truck.Type, result.Type.ToDomain());
        Assert.Equal(truck.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(truck.Manufacturer, result.Manufacturer);
        Assert.Equal(truck.Model, result.Model);
        Assert.Equal(truck.Year, result.Year);
        Assert.Equal(truck.StartingBid, result.StartingBid);
    }
}