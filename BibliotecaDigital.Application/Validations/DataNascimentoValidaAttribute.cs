using System;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaDigital.Application.Validations
{

    public class DataNascimentoValidaAttribute : ValidationAttribute
    {
        private const int IDADE_MINIMA = 18;
        private const int IDADE_MAXIMA = 120;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return ValidationResult.Success;
            }

  
            if (!DateTime.TryParse(value.ToString(), out DateTime dataNascimento))
            {
                return new ValidationResult("Data de nascimento inválida.");
            }

            DateTime dataAtual = DateTime.Now;


            if (dataNascimento > dataAtual)
            {
                return new ValidationResult(
                    "A data de nascimento não pode ser uma data futura."
                );
            }

        
            int idade = CalcularIdade(dataNascimento, dataAtual);


            if (idade < IDADE_MINIMA)
            {
                return new ValidationResult(
                    $"O autor deve ter pelo menos {IDADE_MINIMA} anos de idade. Idade atual: {idade} anos."
                );
            }


            if (idade > IDADE_MAXIMA)
            {
                return new ValidationResult(
                    $"A data de nascimento é muito antiga. A pessoa teria {idade} anos, o que é improvável."
                );
            }


            return ValidationResult.Success;
        }

        private int CalcularIdade(DateTime dataNascimento, DateTime dataReferencia)
        {
            int idade = dataReferencia.Year - dataNascimento.Year;
            if (dataReferencia.Month < dataNascimento.Month || 
                (dataReferencia.Month == dataNascimento.Month && dataReferencia.Day < dataNascimento.Day))
            {
                idade--;
            }
            
            return idade;
        }

        public override string FormatErrorMessage(string name)
        {
            return $"O campo {name} deve conter uma data de nascimento válida (autor com idade entre {IDADE_MINIMA} e {IDADE_MAXIMA} anos).";
        }
    }
}