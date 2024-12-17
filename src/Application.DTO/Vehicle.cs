namespace Application.DTO;

using Application.DTO.Enum;

public class Vehicle
{
    public Guid Id { get; set; }

    public string UniqueIdentifier{ get; set; }

    public VehicleType Type { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public double StartingBid { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfSeats { get; set; }

    public int LoadCapacity { get; set; }
}