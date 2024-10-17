namespace Unit.Tests.Application.Mappers;

using global::Application.DTO;
using global::Application.DTO.Enum;
using global::Application.Services.Mappers;
using Infrastructure.Crosscutting.Utils;

public class HatchBackMapperTests
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
        var hatback = new Vehicle
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
        var result = HatchBackMapper.ToDomain(hatback);

        // Assert
        Assert.Equal(hatback.Id, result.Id);
        Assert.Equal(hatback.Type, result.Type.ToDto());
        Assert.Equal(hatback.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(hatback.Manufacturer, result.Manufacturer);
        Assert.Equal(hatback.Model, result.Model);
        Assert.Equal(hatback.Year, result.Year);
        Assert.Equal(hatback.StartingBid, result.StartingBid);
        Assert.Equal(hatback.NumberOfDoors, result.NumberOfDoors);
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
        var hatback = new global::Domain.Model.Entities.HatchBack
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
        var result = MappToDtoVehicle.ToDto(hatback);

        // Assert
        Assert.Equal(hatback.Id, result.Id);
        Assert.Equal(hatback.Type, result.Type.ToDomain());
        Assert.Equal(hatback.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(hatback.Manufacturer, result.Manufacturer);
        Assert.Equal(hatback.Model, result.Model);
        Assert.Equal(hatback.Year, result.Year);
        Assert.Equal(hatback.StartingBid, result.StartingBid);
        Assert.Equal(hatback.NumberOfDoors, result.NumberOfDoors);
    }
}