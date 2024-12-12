namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class CreateVehiclesService(
    IRepository repository,
    IKafkaProducer kafkaProducer) : ICreateVehiclesService
{
    public async Task<Guid> CreateAsync(DTO.Vehicle dtoVehicle)
    {
        var existentVehicle = await repository.GetByUniqueIdentifierAsync(dtoVehicle.UniqueIdentifier);

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

        await repository.CreateAsync(vehicle);

        await kafkaProducer.ProduceAsync("vehicleTopic", "vehicle as json");

        return vehicle.Id;
    }
}