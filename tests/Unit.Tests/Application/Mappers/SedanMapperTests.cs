namespace Unit.Tests.Application.Mappers;

using global::Application.DTO;
using global::Application.DTO.Enum;
using global::Application.Services.Mappers;
using Infrastructure.Crosscutting.Utils;

public class SedanMapperTests
{
    [Fact]
    public void ToDomain_NullVehicle_ReturnsNull()
    {
        // Act
        var result = HatchBackMapper.ToDomain(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ToDomain_ValidVehicle_ReturnsDomainVehicle()
    {
        // Arrange
        var sedan = new Vehicle
        {
            Id = Guid.NewGuid(),
            Type = VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            NumberOfDoors = RandomValues.GetRandomNumber(5),
        };

        // Act
        var result = HatchBackMapper.ToDomain(sedan);

        // Assert
        Assert.Equal(sedan.Id, result.Id);
        Assert.Equal(sedan.Type, result.Type.ToDto());
        Assert.Equal(sedan.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(sedan.Manufacturer, result.Manufacturer);
        Assert.Equal(sedan.Model, result.Model);
        Assert.Equal(sedan.Year, result.Year);
        Assert.Equal(sedan.StartingBid, result.StartingBid);
        Assert.Equal(sedan.NumberOfDoors, result.NumberOfDoors);
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
        var sedan = new global::Domain.Model.Entities.Sedan
        {
            Id = Guid.NewGuid(),
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            NumberOfDoors = RandomValues.GetRandomNumber(5),
        };

        // Act
        var result = MappToDtoVehicle.ToDto(sedan);

        // Assert
        Assert.Equal(sedan.Id, result.Id);
        Assert.Equal(sedan.Type, result.Type.ToDomain());
        Assert.Equal(sedan.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(sedan.Manufacturer, result.Manufacturer);
        Assert.Equal(sedan.Model, result.Model);
        Assert.Equal(sedan.Year, result.Year);
        Assert.Equal(sedan.StartingBid, result.StartingBid);
        Assert.Equal(sedan.NumberOfDoors, result.NumberOfDoors);
    }
}