namespace Domain.Model.Entities;

using Domain.Model.Abstract;
using Domain.Model.Validators;

public class Truck : Vehicle
{
    public int LoadCapacity { get; set; }

    public override List<string> Validate()
    {
        var errors = base.Validate();

        if (this.LoadCapacity is <= 0)
        {
            errors.Add($"{nameof(this.LoadCapacity)} {CustomValidationMessages.IsRequired}");
        }

        return errors;
    }
}