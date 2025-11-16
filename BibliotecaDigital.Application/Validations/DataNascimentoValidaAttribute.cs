using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.Validations
{
    public class DataNascimentoValidaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("A data de nascimento é obrigatória");
            }

            var dataNascimento = (DateTime)value;
            var hoje = DateTime.Now;
            var idade = hoje.Year - dataNascimento.Year;

            if (dataNascimento > hoje.AddYears(-idade))
            {
                idade--;
            }

            if (dataNascimento > hoje)
            {
                return new ValidationResult("A data de nascimento não pode ser no futuro");
            }

            if (idade < 18)
            {
                return new ValidationResult("O autor deve ter pelo menos 18 anos");
            }

            if (idade > 150)
            {
                return new ValidationResult("Data de nascimento inválida");
            }

            return ValidationResult.Success;
        }
    }
}