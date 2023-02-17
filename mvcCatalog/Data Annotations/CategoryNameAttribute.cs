using System.ComponentModel.DataAnnotations;

namespace mvcCatalog.Data_Annotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class CategoryNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return value.ToString().Length < 60 && value.ToString().Length > 2
            ? ValidationResult.Success
            : new ValidationResult("Length Should be 3-60 symbols ");
    }
}