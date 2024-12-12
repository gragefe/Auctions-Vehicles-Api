namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using AutoMapper;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class UpdateVehiclesService(
    IRepository repository,
    IKafkaProducer kafkaProducer) : IUpdateVehiclesService
{
    public async Task<Guid> UpdateAsync(DTO.Vehicle dtoVehicle)
    {
        var existentVehicle = await repository.GetByIdAsync(dtoVehicle.Id);

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

        await repository.UpdateAsync(vehicle);

        await kafkaProducer.ProduceAsync("vehicleTopic", "vehicle as json");

        return vehicle.Id;
    }
}