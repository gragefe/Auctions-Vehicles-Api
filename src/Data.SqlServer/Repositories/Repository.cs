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

public class Repository : IRepository
{
    private readonly SqlDbContext _context;

    public Repository(
        SqlDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        var vehicleData = VehiclesFactory.ToSql(vehicle);

        await _context.AddAsync(vehicleData);
        await _context.SaveChangesAsync();

        return VehiclesFactory.ToDomain(vehicleData) ?? null;
    }

    public async Task<Vehicle> GetByIdAsync(Guid id)
    {
        var result = await _context.Vehicles.FindAsync(id);

        return result != null ? VehiclesFactory.ToDomain(result) : null;
    }

    public async Task<Vehicle> GetByUniqueIdentifierAsync(string uniqueIdentifier)
    {
        var result = await _context.Vehicles.FirstOrDefaultAsync(v => v.UniqueIdentifier == uniqueIdentifier);

        return result != null ? VehiclesFactory.ToDomain(result) :null;
    }

    public async Task<List<Vehicle>> SearchAsync(SearchContext searchContext)
    {
        var result = await SearchQueryBuilder.BuildSearchQuery(searchContext, _context).ToListAsync();

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

        _context.Vehicles.Update(vehicleData);
    }
}