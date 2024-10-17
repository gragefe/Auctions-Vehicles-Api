namespace Unit.Tests.Application.ApplicationServices;

using global::Domain.Model.Entities;
using global::Domain.Model.Interfaces;
using global::Application.Services;
using global::Application.Services.Mappers;
using Infrastructure.Crosscutting.Utils;
using Infrastructure.Crosscutting.Validations;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using DomainAbstract = global::Domain.Model.Abstract;


public class GetByIdVehiclesServiceTests
{
    private readonly Mock<IRepository> _mockRepository;
    private readonly GetByIdVehiclesService _getByIdVehiclesService;

    public GetByIdVehiclesServiceTests()
    {
        _mockRepository = new Mock<IRepository>();

        _getByIdVehiclesService = new GetByIdVehiclesService(
            this._mockRepository.Object
        );
    }

    [Fact]
    public async Task GetByIdAsync_NonexistentVehicle_ThrowsNonExistentVehicleValidationException()
    {
        // Arrange
        var expectedErrorsFound = 1;
        var vehicleId = Guid.NewGuid();

        _mockRepository
             .Setup(repo => repo.GetByIdAsync(vehicleId))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)null));

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _getByIdVehiclesService.GetByIdAsync(vehicleId));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<CustomValidationException>(exception);
        Assert.Equal(expectedErrorsFound, (exception as CustomValidationException).ErrorsFound);
        Assert.Equal(CustomValidationMessages.NonExistentVehicle, (exception as CustomValidationException).Errors.ElementAt(0));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldGetVehicle_WhenValid()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var uniqueIdentifier = RandomValues.GetRandomString(8);

        var existentVehicle = new HatchBack()
        {
            Id = vehicleId,
            Type = global::Domain.Model.Enum.VehicleType.HatchBack,
            NumberOfDoors = 5,
            UniqueIdentifier = uniqueIdentifier,
            Manufacturer = RandomValues.GetRandomString(10),
            Model = RandomValues.GetRandomString(10),
            Year = 2024,
            StartingBid = RandomValues.GetRandomNumber(300),
        };

        _mockRepository
             .Setup(repo => repo.GetByIdAsync(vehicleId))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)existentVehicle));

        var existentVehicleDto = existentVehicle.ToDto();

        // Act
        var result = await _getByIdVehiclesService.GetByIdAsync(vehicleId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existentVehicleDto.Id, result.Id);
        Assert.Equal(existentVehicleDto.Type, result.Type);
        Assert.Equal(existentVehicleDto.UniqueIdentifier, result.UniqueIdentifier);
        Assert.Equal(existentVehicleDto.Manufacturer, result.Manufacturer);
        Assert.Equal(existentVehicleDto.Model, result.Model);
        Assert.Equal(existentVehicleDto.Year, result.Year);
        Assert.Equal(existentVehicleDto.StartingBid, result.StartingBid);
        Assert.Equal(existentVehicleDto.NumberOfDoors, result.NumberOfDoors);
    }
}