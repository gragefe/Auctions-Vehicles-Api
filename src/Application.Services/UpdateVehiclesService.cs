namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using AutoMapper;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class UpdateVehiclesService : IUpdateVehiclesService
{
    private readonly IRepository _repository;
    private readonly IKafkaProducer _KafkaProducer;

    public UpdateVehiclesService(
        IRepository repository,
              IKafkaProducer kafkaProducer)
    {
        _repository = repository;
        _KafkaProducer = kafkaProducer;
    }

    public async Task<Guid> UpdateAsync(DTO.Vehicle dtoVehicle)
    {
        var existentVehicle = await _repository.GetByIdAsync(dtoVehicle.Id);

        if (existentVehicle == null)
        {
            throw new CustomValidationException(CustomValidationMessages.NonExistentVehicle);
        }

        var vehicle = VehiclesFactory.ToDomain(dtoVehicle);

        var validationErrors = vehicle.Validate();

        if (validationErrors.Count > 0)
        {
            throw new CustomValidationException(validationErrors);
        }

        await _repository.UpdateAsync(vehicle);

        await _KafkaProducer.ProduceAsync("vehicleTopic", "vehicle as json");

        return vehicle.Id;
    }
}