namespace Domain.Model.Interfaces;

using Domain.Model.Abstract;

public interface IRepository
{
    Task<Vehicle> CreateAsync(Vehicle Vehicle);

    Task UpdateAsync(Vehicle Vehicle);

    Task<Vehicle> GetByIdAsync(Guid id);

    Task<Vehicle> GetByUniqueIdentifierAsync(string uniqueIdentifier);

    Task<List<Vehicle>> SearchAsync(SearchContext searchContext);
}