namespace Domain.Model.Entities;

using Domain.Model.Abstract;
using Domain.Model.CommonValidators;
using Domain.Model.Validators;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Suv : Vehicle
{
    public int NumberOfSeats { get; set; }

    public override List<string> Validate()
    {
        var errors = base.Validate();

        if (this.NumberOfSeats is <= 0)
        {
            errors.Add($"{nameof(this.NumberOfSeats)} {CustomValidationMessages.IsRequired}");
        }

        return errors;
    }
}