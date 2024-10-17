namespace Unit.Tests.Application.Mappers;

using global::Application.DTO;
using global::Application.DTO.Enum;
using global::Application.Services.Mappers;
using Infrastructure.Crosscutting.Utils;

public class SuvMapperTests
{
    [Fact]
    public void ToDomain_NullVehicle_ReturnsNull()
    {
        // Act
        var result = SuvMapper.ToDomain(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ToDomain_ValidVehicle_ReturnsDomainVehicle()
    {
        // Arrange
        var Suv = new Vehicle
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
        var result = SuvMapper.ToDomain(Suv);

        // Assert
        Assert.Equal(Suv.Id, result.Id);
        Assert.Equal(Suv.Type, result.Type.ToDto());
        Assert.Equal(Suv.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(Suv.Manufacturer, result.Manufacturer);
        Assert.Equal(Suv.Model, result.Model);
        Assert.Equal(Suv.Year, result.Year);
        Assert.Equal(Suv.StartingBid, result.StartingBid);

        Assert.NotNull(result);
    }

    [Fact]
    public void ToDto_NullVehicle_ReturnsNull()
    {
        // Act
        var result = MappToDtoVehicle.ToDto(null);

        // Assert
        Assert.Null(result);
    }
}