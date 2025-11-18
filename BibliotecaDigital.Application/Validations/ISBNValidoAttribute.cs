using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.Validations
{

    public class ISBNValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string isbn = value.ToString()!.Trim();
            

            string isbnLimpo = isbn.Replace("-", "").Replace(" ", "");

            if (isbnLimpo.Length != 10 && isbnLimpo.Length != 13)
            {
                return new ValidationResult(
                    "ISBN deve conter 10 ou 13 dígitos. Formato aceito: XXX-XXXXXXXXXX ou apenas números."
                );
            }


            if (!long.TryParse(isbnLimpo, out _))
            {
                return new ValidationResult(
                    "ISBN deve conter apenas números e hífens."
                );
            }


            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O campo {name} possui um formato inválido de ISBN.";
        }
    }
}