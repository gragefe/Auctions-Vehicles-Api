namespace Application.Services.Interfaces;

using Application.DTO;

public interface IUpdateVehiclesService
{
    public Task<Guid> UpdateAsync(Vehicle DtoVehicle);
}