namespace Domain.Model.Abstract;

using Domain.Model.Enum;
using Domain.Model.Validators;

public abstract class Vehicle
{
    public Guid Id { get; set; }

    public string UniqueIdentifier { get; set; }

    public VehicleType Type { get; set; }

    public string Manufacturer { get; set; }

    public string Model { get; set; }

    public int Year { get; set; }

    public float StartingBid { get; set; }

    public int NumberOfDoors { get; set; }

    public virtual List<string> Validate()
    {
        var errors = new List<string>();

        if (this.Type == VehicleType.None)
        {
            errors.Add($"{nameof(this.Type)} {CustomValidationMessages.IsRequired}");
        }

        if (string.IsNullOrEmpty(this.Manufacturer))
        {
            errors.Add($"{nameof(this.Manufacturer)} {CustomValidationMessages.IsRequired}");
        }

        if (string.IsNullOrEmpty(this.Model))
        {
            errors.Add($"{nameof(this.Model)} {CustomValidationMessages.IsRequired}");
        }

        if (this.Year < 1000 || this.Year > 9999)
        {
            errors.Add($"{nameof(this.Year)}{CustomValidationMessages.MustHaveFourDgits}");
        }

        if (this.Year > DateTime.Now.Year)
        {
            errors.Add($"{nameof(this.Year)} {CustomValidationMessages.CannotBeFutureYear}");
        }

        if (this.StartingBid <= 0)
        {
            errors.Add($"{nameof(this.StartingBid)}{CustomValidationMessages.MustBeGreaterThanZero}");
        }

        return errors;
    }
}