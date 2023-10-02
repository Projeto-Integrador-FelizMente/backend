using FelizMente.Model;
using FluentValidation;

namespace FelizMente.Validator
{
    public class PostagemValidator : AbstractValidator<Postagem>
    {
        public PostagemValidator()
        {
            RuleFor(p => p.Titulo)
              .NotEmpty()
              .MinimumLength(5)
              .MaximumLength(255);

            RuleFor(p => p.Estado)
               .NotEmpty();

            RuleFor(p => p.Texto)
              .NotEmpty()
              .MinimumLength(5);

            RuleFor(p => p.Link)
              .NotEmpty()
              .MinimumLength(5)
              .MaximumLength(255);

        }
    }
}
