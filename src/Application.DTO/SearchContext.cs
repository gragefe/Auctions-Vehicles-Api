namespace Application.DTO;

using Application.DTO.Enum;

public class SearchContext
{
    public VehicleType? Type { get; set; }

    public string? Manufacturer { get; set; }

    public string? Model { get; set; }

    public int? Year { get; set; }
}