namespace Domain.Model.CommonValidators;

using Domain.Model.Validators;

public static class NumberOfDoorsValidator
{
    public static List<string> Validate(int numberOfDoors)
    {
        var errors = new List<string>();

        if (numberOfDoors is <= 0)
        {
            errors.Add($"{CustomValidationMessages.NumerOfDoors} {CustomValidationMessages.IsRequired}");
        }

        return errors;
    }
}