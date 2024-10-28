namespace Application.Services.Mappers;

using DomainEntities = Domain.Model.Entities;
public static class SearchContextMapper
{
    public static Domain.Model.SearchContext ToDomain(this DTO.SearchContext searchContext)
    {
        if (searchContext == null)
        {
            return null;
        }

        return new Domain.Model.SearchContext
        {
            VehicleType = searchContext.VehicleType?.ToDomain(),
            Manufacturer = searchContext.Manufacturer,
            Model = searchContext.Model,
            Year = searchContext.Year
        };
    }
}