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

public class CreateVehiclesServiceTests
{
    private readonly Mock<IRepository> _mockRepository;
    private readonly Mock<IKafkaProducer> _mockKafkaProducer;
    private readonly CreateVehiclesService _createVehiclesService;


    public CreateVehiclesServiceTests()
    {
        _mockRepository = new Mock<IRepository>();
        _mockKafkaProducer = new Mock<IKafkaProducer>();

        _createVehiclesService = new CreateVehiclesService(
            this._mockRepository.Object,
             this._mockKafkaProducer.Object
        );
    }

    [Fact]
    public async Task CreateAsync_ExistentVehicle_ThrowsExistentUniqueIdentifierValidationException()
    {
        // Arrange
        var expectedErrorsFound = 1;
        var uniqueIdentifier = RandomValues.GetRandomString(8);

        var newVehicle = new DTO.Vehicle
        {
            UniqueIdentifier = uniqueIdentifier
        };

        var existentVehicle = new DomainEntities.HatchBack()
        {
            UniqueIdentifier = uniqueIdentifier
        };

        _mockRepository
            .Setup(repo => repo.GetByUniqueIdentifierAsync(uniqueIdentifier))
            .Returns(Task.FromResult((DomainAbstract.Vehicle)existentVehicle));

        // Act
        var exception = await Record.ExceptionAsync(() =>
            _createVehiclesService.CreateAsync(newVehicle));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<CustomValidationException>(exception);
        Assert.Equal(expectedErrorsFound, (exception as CustomValidationException).ErrorsFound);
        Assert.Equal(CustomValidationMessages.ExistentUniqueIdentifier, (exception as CustomValidationException).Errors.ElementAt(0));
        _mockKafkaProducer.Verify(producer => producer.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task CreateAsync_VehicleValidationFails_ThrowCustomValidationException()
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
            _createVehiclesService.CreateAsync(newVehicle));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<CustomValidationException>(exception);
        _mockKafkaProducer.Verify(producer => producer.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

    }

    [Fact]
    public async Task CreateAsync_ShouldCreateVehicle_WhenValid()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var uniqueIdentifier = RandomValues.GetRandomString(8);

        var newVehicle = new DTO.Vehicle
        {
            Id = vehicleId,
            Type = DTO.Enum.VehicleType.HatchBack,
            NumberOfDoors = 5,
            UniqueIdentifier = uniqueIdentifier,
            Manufacturer = RandomValues.GetRandomString(10),
            Model = RandomValues.GetRandomString(10),
            Year = 2024,
            StartingBid = RandomValues.GetRandomNumber(300),
        };

        _mockRepository
             .Setup(repo => repo.GetByUniqueIdentifierAsync(newVehicle.UniqueIdentifier))
             .Returns(Task.FromResult((DomainAbstract.Vehicle)null));

        // Act
        var result = await _createVehiclesService.CreateAsync(newVehicle);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicleId, result);
        _mockKafkaProducer.Verify(producer => producer.ProduceAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

    }
}