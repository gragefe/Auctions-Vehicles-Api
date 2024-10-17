namespace Unit.Tests.Application.ApplicationServices;
using global::Domain.Model.Interfaces;
using global::Application.Services;
using global::Application.Services.Interfaces;
using Infrastructure.Crosscutting.Utils;
using Infrastructure.Crosscutting.Validations;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using DomainAbstract = global::Domain.Model.Abstract;
using DomainEntities = global::Domain.Model.Entities;
using DTO = global::Application.DTO;


public class UpdateVehiclesServiceTests
{
    private readonly Mock<IRepository> _mockRepository;
    private readonly Mock<IKafkaProducer> _mockKafkaProducer;
    private readonly UpdateVehiclesService _updateVehiclesService;

    public UpdateVehiclesServiceTests()
    {
        _mockRepository = new Mock<IRepository>();
        _mockKafkaProducer = new Mock<IKafkaProducer>();

        _updateVehiclesService = new UpdateVehiclesService(
            this._mockRepository.Object,
             this._mockKafkaProducer.Object
        );
    }

    [Fact]
    public async Task UpdateAsync_NonexistentVehicle_ThrowsNonExistentVehicleValidationException()
    {
        // Arrange
        var expectedErrorsFound = 1;

        var vehicleToUpdate = new DTO.Vehicle
        {
            Id = Guid.NewGuid(),
            Type = DTO.Enum.VehicleType.HatchBack,
            NumberOfDoors = 5,
            Manufacturer = RandomValues.GetRandomString(10),
            Model = RandomValues.GetRandomString(10),
            Year = 2024,
            StartingBid = RandomValues.GetRandomNumber(300),
            UniqueIdentifier = RandomValues.GetRandomString(8)
        };

        _mockRepository
             .Setup(repo => repo.GetByUniqueIdentifierAsync(vehicleToUpdate.UniqueIdentifier))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)null));

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _updateVehiclesService.UpdateAsync(vehicleToUpdate));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<CustomValidationException>(exception);
        Assert.Equal(expectedErrorsFound, (exception as CustomValidationException).ErrorsFound);
        Assert.Equal(CustomValidationMessages.NonExistentVehicle, (exception as CustomValidationException).Errors.ElementAt(0));
    }

    [Fact]
    public async Task UpdateAsync_VehicleValidationFails_ThrowCustomValidationException()
    {
        // Arrange
        var newVehicle = new DTO.Vehicle
        {
            Type = DTO.Enum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(8)
        };

        _mockRepository
             .Setup(repo => repo.GetByUniqueIdentifierAsync(newVehicle.UniqueIdentifier))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)null));

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _updateVehiclesService.UpdateAsync(newVehicle));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<CustomValidationException>(exception);
        _mockKafkaProducer.Verify(producer => producer.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateVehicle_WhenValid()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var uniqueIdentifier = RandomValues.GetRandomString(8);

        var newVehicle = new DTO.Vehicle
        {
            Id = vehicleId,
            Type = DTO.Enum.VehicleType.HatchBack,
            NumberOfDoors = 3,
            UniqueIdentifier = uniqueIdentifier,
            Manufacturer = RandomValues.GetRandomString(10),
            Model = RandomValues.GetRandomString(10),
            Year = 2024,
            StartingBid = RandomValues.GetRandomNumber(300),
        };

        var existentVehicle = new DomainEntities.HatchBack()
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
             .Setup(repo => repo.GetByIdAsync(newVehicle.Id))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)existentVehicle));

        // Act
        await _updateVehiclesService.UpdateAsync(newVehicle);

        // Assert
        Assert.NotNull(newVehicle);
        Assert.Equal(vehicleId, newVehicle.Id);
        _mockKafkaProducer.Verify(producer => producer.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

    }

}