namespace Data.SqlServer.Queries;

using Data.SqlServer.Entities;
using Domain.Model;

internal static class SearchQueryBuilder
{
    internal static IQueryable<Vehicle> BuildSearchQuery(SearchContext searchContext, SqlDbContext db)
    {
        IQueryable<Vehicle> query = db.Vehicles;

        if (searchContext.VehicleType != null && searchContext.VehicleType != Domain.Model.Enum.VehicleType.None)
        {
            query = query.Where(c => c.Type == (Enum.VehicleType)searchContext.VehicleType);
        }

        if (!string.IsNullOrEmpty(searchContext.Manufacturer))
        {
            query = query.Where(c => c.Manufacturer == searchContext.Manufacturer);
        }

        if (!string.IsNullOrEmpty(searchContext.Model))
        {
            query = query.Where(c => c.Model == searchContext.Model);
        }

        if (searchContext.Year > 0)
        {
            query = query.Where(c => c.Year == searchContext.Year);
        }

        return query;
    }
}