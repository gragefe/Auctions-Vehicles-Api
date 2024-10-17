namespace Application.Services.Interfaces;

using Application.DTO;

public interface IGetByIdVehiclesService
{
    public Task<Vehicle> GetByIdAsync(Guid Id);
}