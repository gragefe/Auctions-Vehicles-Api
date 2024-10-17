namespace Domain.Model;

using Domain.Model.Enum;

public class SearchContext
{
    public VehicleType? VehicleType { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }
}