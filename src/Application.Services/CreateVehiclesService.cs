namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class CreateVehiclesService : ICreateVehiclesService
{
    private readonly IRepository _repository;
    private readonly IKafkaProducer _KafkaProducer;

    public CreateVehiclesService(
        IRepository repository,
        IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _KafkaProducer = kafkaProducer;
    }

    public async Task<Guid> CreateAsync(DTO.Vehicle dtoVehicle)
    {
        var existentVehicle = await _repository.GetByUniqueIdentifierAsync(dtoVehicle.UniqueIdentifier);

        if (existentVehicle != null)
        {
            throw new CustomValidationException(CustomValidationMessages.ExistentUniqueIdentifier);
        }

        var vehicle = VehiclesFactory.ToDomain(dtoVehicle);

        var validationErrors = vehicle.Validate();

        if (validationErrors.Count > 0)
        {
            throw new CustomValidationException(validationErrors);
        }

        await _repository.CreateAsync(vehicle);

        await _KafkaProducer.ProduceAsync("vehicleTopic", "vehicle as json");

        return vehicle.Id;
    }
}