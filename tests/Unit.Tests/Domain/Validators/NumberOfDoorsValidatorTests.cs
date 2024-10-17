namespace Unit.Tests.Domain.Validators;

using global::Application.Services.Mappers;
using global::Domain.Model.CommonValidators;
using global::Domain.Model.Validators;

public class NumberOfDoorsValidatorTests
{
    [Fact]
    public void Validate_WhenNumberOfDoorsIsValid_ShouldReturnEmptyList()
    {
        // Arrange
        int validNumberOfDoors = 4;

        // Act
        var result = NumberOfDoorsValidator.Validate(validNumberOfDoors);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void Validate_WhenNumberOfDoorsIsZero_ShouldReturnErrorMessage()
    {
        // Arrange
        int invalidNumberOfDoors = 0;
        var expectedErrorMessage = $"{CustomValidationMessages.NumerOfDoors} {CustomValidationMessages.IsRequired}";

        // Act
        var result = NumberOfDoorsValidator.Validate(invalidNumberOfDoors);

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedErrorMessage, result[0]);
    }

    [Fact]
    public void Validate_WhenNumberOfDoorsIsNegative_ShouldReturnErrorMessage()
    {
        // Arrange
        int invalidNumberOfDoors = -2;
        var expectedErrorMessage = $"{CustomValidationMessages.NumerOfDoors} {CustomValidationMessages.IsRequired}";

        // Act
        var result = NumberOfDoorsValidator.Validate(invalidNumberOfDoors);

        // Assert
        Assert.Single(result);
        Assert.Equal(expectedErrorMessage, result[0]);
    }

    [Fact]
    public void Validate_WhenNumberOfDoorsIsPositive_ShouldReturnNoErrors()
    {
        // Arrange
        int validNumberOfDoors = 2;

        // Act
        var result = NumberOfDoorsValidator.Validate(validNumberOfDoors);

        // Assert
        Assert.Empty(result);
    }
}