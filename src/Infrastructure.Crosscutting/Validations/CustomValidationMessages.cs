namespace Infrastructure.Crosscutting.Validations;

public static class CustomValidationMessages
{
    public const string IsRequired = "Is required.";

    public const string ExistentUniqueIdentifier = "Unique identifier already in use.";

    public const string MustHaveFourDgits = "Must have 4 digits.";

    public const string CannotBeFutureYear = "Cannot be greater than the current year.";

    public const string MustBeGreaterThanZero = "Must be greater than zero.";

    public const string NonExistentVehicle = "Non-existent vehicle.";
    
    public const string InvalidVehicleId = "The vehicle Id is invalid, it needs to match with the vehicle Id in the body.";

    public const string NonExistentVehicleType = "Inavalid vehicle type.";
}