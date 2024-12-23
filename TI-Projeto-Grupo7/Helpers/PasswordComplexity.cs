using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TI_Projeto_Grupo7.Helpers
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                // Minimum length requirement
                if (password.Length < 10)
                    return new ValidationResult("The password must be at least 10 characters long.");

                // At least one uppercase letter
                if (!Regex.IsMatch(password, @"[A-Z]"))
                    return new ValidationResult("The password must contain at least one uppercase letter.");

                // At least one special character
                if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?\"":{ }|<>]"))
                    return new ValidationResult("The password must contain at least one special character.");

                // At least one digit
                if (!Regex.IsMatch(password, @"\d"))
                    return new ValidationResult("The password must contain at least one digit.");
            }

            return ValidationResult.Success;
        }
    }
}