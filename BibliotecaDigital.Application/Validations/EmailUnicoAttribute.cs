using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BibliotecaDigital.Application.Validations
{

    public class EmailUnicoAttribute : ValidationAttribute
    {

        private static readonly string[] DominiosProibidos = 
        {
            "tempmail.com",
            "throwaway.email",
            "guerrillamail.com",
            "10minutemail.com",
            "mailinator.com"
        };

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string email = value.ToString()!.Trim().ToLower();


            if (!IsValidEmailFormat(email))
            {
                return new ValidationResult(
                    "O email deve estar em um formato válido (exemplo: usuario@dominio.com)."
                );
            }


            string dominio = email.Split('@')[1];
            if (Array.Exists(DominiosProibidos, d => d.Equals(dominio, StringComparison.OrdinalIgnoreCase)))
            {
                return new ValidationResult(
                    $"Emails do domínio '{dominio}' não são permitidos. Use um email profissional ou pessoal válido."
                );
            }


            if (email.Contains("..") || email.StartsWith(".") || email.EndsWith("."))
            {
                return new ValidationResult(
                    "O email não pode conter pontos consecutivos ou iniciar/terminar com ponto."
                );
            }


            return ValidationResult.Success;
        }

        private bool IsValidEmailFormat(string email)
        {

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O campo {name} deve conter um email válido e único.";
        }
    }
}