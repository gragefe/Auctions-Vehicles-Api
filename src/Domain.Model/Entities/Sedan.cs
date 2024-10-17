namespace Domain.Model.Entities;

using Domain.Model.Abstract;
using Domain.Model.CommonValidators;

public class Sedan : Vehicle
{
    public override List<string> Validate()
    {
        var errors = base.Validate();

        var customValidationErrors = NumberOfDoorsValidator.Validate(this.NumberOfDoors);

        if (customValidationErrors.Count > 0)
        {
            errors.AddRange(customValidationErrors);
        }

        return errors;
    }
}