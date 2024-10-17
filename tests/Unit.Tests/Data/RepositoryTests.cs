namespace Unit.Tests.Data;

using global::Data.SqlServer;
using global::Domain.Model;
using Infrastructure.Crosscutting.Utils;
using Microsoft.EntityFrameworkCore;
using DomainModelEntities = global::Domain.Model.Entities;
using DomainModelEnum = global::Domain.Model.Enum;

public class RepositoryTests
{
    private readonly SqlDbContext _context;
    private readonly Repository _repository;

    public RepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SqlDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new SqlDbContext(options);
        _repository = new Repository(_context);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedVehicle()
    {
        // Arrange
        var vehicle = new DomainModelEntities.HatchBack 
        { 
            Type = DomainModelEnum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(6),
            Manufacturer = RandomValues.GetRandomString(6),
            Id = Guid.NewGuid() 
        };

        // Act
        var result = await _repository.CreateAsync(vehicle);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicle.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnVehicle_WhenVehicleExists()
    {
        // Arrange
        var vehicle = new DomainModelEntities.HatchBack
        {
            Type = DomainModelEnum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(6),
            Manufacturer = RandomValues.GetRandomString(6),
            Id = Guid.NewGuid()
        };

        // Act
        await _repository.CreateAsync(vehicle);
        var result = await _repository.GetByIdAsync(vehicle.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicle.Id, result.Id);
    }
    
    [Fact]
    public async Task GetByUniqueIdentifierAsync_ShouldReturnVehicle_WhenVehicleExists()
    {
        // Arrange
        var vehicle = new DomainModelEntities.HatchBack
        {
            Type = DomainModelEnum.VehicleType.HatchBack,
            UniqueIdentifier = RandomValues.GetRandomString(6),
            Model = RandomValues.GetRandomString(6),
            Manufacturer = RandomValues.GetRandomString(6),
            Id = Guid.NewGuid()
        };

        // Act
        await _repository.CreateAsync(vehicle);
        var result = await _repository.GetByUniqueIdentifierAsync(vehicle.UniqueIdentifier);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vehicle.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();

        // Act
        var result = await _repository.GetByIdAsync(vehicleId);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task GetByUniqueIdentifierAsync_ShouldReturnNull_WhenVehicleDoesNotExist()
    {
        // Arrange
        var uniqueIdentifier = RandomValues.GetRandomString(6);

        // Act
        var result = await _repository.GetByUniqueIdentifierAsync(uniqueIdentifier);

        // Assert
        Assert.Null(result);
    }

}