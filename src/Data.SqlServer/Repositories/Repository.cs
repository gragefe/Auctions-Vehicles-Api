namespace Data.SqlServer;

using Data.SqlServer.Mappers;
using Data.SqlServer.Queries;
using Domain.Model;
using Domain.Model.Abstract;
using Domain.Model.Interfaces;
using Infrastructure.Crosscutting.Validations;
using Microsoft.EntityFrameworkCore;

public class Repository(SqlDbContext context) : IRepository
{
    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        var vehicleData = VehiclesFactory.ToSql(vehicle);

        await context.AddAsync(vehicleData);
        await context.SaveChangesAsync();

        return VehiclesFactory.ToDomain(vehicleData) ?? null;
    }

    public async Task<Vehicle> GetByIdAsync(Guid id)
    {
        var result = await context.Vehicles.FindAsync(id);

        return result != null ? VehiclesFactory.ToDomain(result) : null;
    }

    public async Task<Vehicle> GetByUniqueIdentifierAsync(string uniqueIdentifier)
    {
        var result = await context.Vehicles.FirstOrDefaultAsync(v => v.UniqueIdentifier == uniqueIdentifier);

        return result != null ? VehiclesFactory.ToDomain(result) :null;
    }

    public async Task<List<Vehicle>> SearchAsync(SearchContext searchContext)
    {
        var result = await SearchQueryBuilder.BuildSearchQuery(searchContext, context).ToListAsync();

        var vehicles = new List<Vehicle>();

        foreach (var item in result)
        {
            vehicles.Add(VehiclesFactory.ToDomain(item));
        }

        return vehicles;
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        var vehicleData = await context.Vehicles.FindAsync(vehicle.Id);

        if (vehicleData == null)
        {
            throw new CustomValidationException(CustomValidationMessages.NonExistentVehicle);
        }
        
        vehicleData.Model = vehicle.Model;
        vehicleData.Type = vehicle.Type.ToDto();
        vehicleData.Model = vehicle.Model;
        vehicleData.Year = vehicle.Year;
        vehicleData.UniqueIdentifier = vehicle.UniqueIdentifier;
        vehicleData.Manufacturer = vehicle.Manufacturer;
        vehicleData.StartingBid = vehicle.StartingBid;
        vehicleData.NumberOfDoors = vehicle.NumberOfDoors;

        context.Vehicles.Update(vehicleData);
        await context.SaveChangesAsync();
    }
}