using CP3.Domain.Interfaces.Dtos;
using FluentValidation;

namespace CP3.Application.Dtos
{
    public class BarcoDto : IBarcoDto
    {
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Tamanho { get; set; }
        public void Validate()
        {
            var validateResult = new BarcoDtoValidation().Validate(this);
            if(validateResult.IsValid )
            {
                throw new ArgumentException(string.Join(" e ", validateResult.Errors.Select(x => x.ErrorMessage)));
            }
        }
    }

    internal class BarcoDtoValidation : AbstractValidator<BarcoDto>
    {
        public BarcoDtoValidation()
        {
            RuleFor(x => x.Nome)
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.Nome)} deve ter no minimo 3 caracteres")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Nome)} não pode ser vazio");

            RuleFor(x => x.Modelo)
                .MinimumLength(3).WithMessage(x => $"O campo {nameof(x.Modelo)} deve ter no minimo 3 caracteres")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Modelo)} não pode ser vazio");

            RuleFor(x => x.Ano)
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage(x => $"O campo {nameof(x.Ano)} não pode ser no futuro")
                .NotEmpty().WithMessage(x => $"O campo {nameof(x.Ano)} não pode ser vazio");
        }
    }

}
