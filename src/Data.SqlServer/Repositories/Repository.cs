namespace Data.SqlServer;

using Data.SqlServer.Mappers;
using Data.SqlServer.Queries;
using Domain.Model;
using Domain.Model.Abstract;
using Domain.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        var vehicleData = VehiclesFactory.ToSql(vehicle);

        context.Vehicles.Update(vehicleData);
    }
}