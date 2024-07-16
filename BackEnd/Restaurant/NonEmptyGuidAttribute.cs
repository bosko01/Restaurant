using System;

[AttributeUsage(AttributeTargets.Property)]
public class NonEmptyGuidAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) =>
        ((value is Guid guid) && guid == default) ?
            new ValidationResult(FormatErrorMessage(validationContext.DisplayName)) :
            ValidationResult.Success;
}