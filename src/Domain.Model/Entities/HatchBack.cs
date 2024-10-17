namespace Domain.Model.Entities;

using Domain.Model.Abstract;
using Domain.Model.CommonValidators;

public class HatchBack : Vehicle
{
    public override List<string> Validate()
    {
        var erros = base.Validate();

       var customValidationErrors = NumberOfDoorsValidator.Validate(this.NumberOfDoors);

        if (customValidationErrors.Count > 0)
        {
            erros.AddRange(customValidationErrors);
        }

        return erros;
    }
}