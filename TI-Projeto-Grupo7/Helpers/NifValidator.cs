using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Application.Data;
using TI_Projeto_Grupo7.Services;

namespace TI_Projeto_Grupo7.Helpers
{
    public class NifValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("NIF is required.");

            string nif = value.ToString();

            // Check if NIF has exactly 9 digits
            if (!Regex.IsMatch(nif, @"^\d{9}$"))
                return new ValidationResult("NIF must be a 9-digit number.");

            // Validate the NIF formula
            if (!IsValidNif(nif))
                return new ValidationResult("Invalid NIF.");

            // Check if NIF already exists using UsersService
            var userService = (UsersService)validationContext.GetService(typeof(UsersService));
            if (userService != null && IsNifRegistered(userService, nif))
                return new ValidationResult("This NIF is already registered.");

            return ValidationResult.Success;
        }

        private bool IsValidNif(string nif)
        {
            var digits = nif.Select(c => int.Parse(c.ToString())).ToArray();

            // First digit validation (adjust based on NIF rules)
            if (!"123578".Contains(digits[0].ToString()))
                return false;

            int checkSum = 0;
            for (int i = 0; i < 8; i++)
            {
                checkSum += digits[i] * (9 - i);
            }

            int checkDigit = 11 - (checkSum % 11);
            if (checkDigit >= 10) checkDigit = 0;

            return checkDigit == digits[8];
        }

        private bool IsNifRegistered(UsersService userService, string nif)
        {
            var users = userService.Get()?.Results; 
            return users != null && users.Any(u => u.nif == int.Parse(nif));
        }
    }
}