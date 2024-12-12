namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class GetByIdVehiclesService(IRepository repository) : IGetByIdVehiclesService
{
    public async Task<DTO.Vehicle> GetByIdAsync(Guid id)
    {
        var vehicle = await repository.GetByIdAsync(id);

        if (vehicle == null) throw new CustomValidationException(CustomValidationMessages.NonExistentVehicle);

        var vehicleDto = VehiclesFactory.ToDto(vehicle);

        return vehicleDto;
    }
}