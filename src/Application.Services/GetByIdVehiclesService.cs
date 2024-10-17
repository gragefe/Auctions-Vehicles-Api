namespace Application.Services;

using Application.Services.Interfaces;
using Application.Services.Mappers;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using DTO = Application.DTO;


public class GetByIdVehiclesService : IGetByIdVehiclesService
{
    private readonly IRepository _repository;

    public GetByIdVehiclesService(
        IRepository repository)
    {
        _repository = repository;
    }

    public async Task<DTO.Vehicle> GetByIdAsync(Guid id)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null) throw new CustomValidationException(CustomValidationMessages.NonExistentVehicle);

        var vehicleDto = VehiclesFactory.ToDto(vehicle);

        return vehicleDto;
    }
}