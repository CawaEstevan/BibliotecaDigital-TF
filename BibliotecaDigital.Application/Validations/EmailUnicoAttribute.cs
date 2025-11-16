using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.Validations
{
    public class EmailUnicoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var email = value.ToString()!;
            
            // Validação simples: email deve conter @ e um domínio válido
            if (!email.Contains("@") || !email.Contains("."))
            {
                return new ValidationResult("O email deve ser válido e conter @ e domínio");
            }

            return ValidationResult.Success;
        }
    }
}