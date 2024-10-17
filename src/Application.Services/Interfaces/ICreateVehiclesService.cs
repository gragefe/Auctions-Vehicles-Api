namespace Application.Services.Interfaces;

using Application.DTO;

public interface ICreateVehiclesService
{
    public Task<Guid> CreateAsync(Vehicle DtoVehicle);
}