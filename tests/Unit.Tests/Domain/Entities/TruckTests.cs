namespace Unit.Tests.Domain.Entities;

using global::Domain.Model.Entities;
using global::Domain.Model.Validators;
using Infrastructure.Crosscutting.Utils;
using System;

public class TruckTests
{
    [Fact]
    public void Validate_WhenNumberOfSeatsIsGreaterThanZero_ShouldReturnNoErrors()
    {
        // Arrange
        var expectedErrorsFound = 1;

        var truck = new Truck
        {
            Id = Guid.NewGuid(),
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            LoadCapacity = 1
        };

        // Act
        var result = truck.Validate();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Validate_WhenNumberOfSeatsIsNegative_ShouldReturnErrorMessage()
    {
        // Arrange
        var truck = new Truck
        {
            Id = Guid.NewGuid(),
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            LoadCapacity = -1
        };

        var expectedErrorMessage = $"{nameof(truck.LoadCapacity)} {CustomValidationMessages.IsRequired}";

        // Act
        var result = truck.Validate();

        // Assert
        Assert.Contains(expectedErrorMessage, result);
    }

    [Fact]
    public void Validate_WhenNumberOfSeatsIsZero_ShouldReturnErrorMessage()
    {
        // Arrange
        var truck = new Truck
        {
            Id = Guid.NewGuid(),
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(10),
            Manufacturer = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(4),
            Year = 2020,
            StartingBid = RandomValues.GetRandomNumber(800),
            LoadCapacity = 0
        };

        var expectedErrorMessage = $"{nameof(truck.LoadCapacity)} {CustomValidationMessages.IsRequired}";

        // Act
        var result = truck.Validate();

        // Assert
        Assert.Contains(expectedErrorMessage, result);
    }

}