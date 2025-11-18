using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.Validations
{

    public class AnoPublicacaoRealistaAttribute : ValidationAttribute
    {
        private const int ANO_INVENCAO_IMPRENSA = 1450;
        private readonly int _anoMinimo;


        public AnoPublicacaoRealistaAttribute(int anoMinimo = ANO_INVENCAO_IMPRENSA)
        {
            _anoMinimo = anoMinimo;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!int.TryParse(value.ToString(), out int ano))
            {
                return new ValidationResult("Ano de publicação deve ser um número válido.");
            }

            int anoAtual = DateTime.Now.Year;

            if (ano < _anoMinimo)
            {
                return new ValidationResult(
                    $"Ano de publicação não pode ser anterior a {_anoMinimo} (invenção da imprensa por Gutenberg)."
                );
            }


            if (ano > anoAtual + 1)
            {
                return new ValidationResult(
                    $"Ano de publicação não pode ser superior a {anoAtual + 1}."
                );
            }


            return ValidationResult.Success;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O campo {name} deve conter um ano de publicação válido e realista.";
        }
    }
}