using FelizMente.Model;
using FluentValidation;

namespace FelizMente.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Nome)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(255);


            RuleFor(u => u.Usuario)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(255);


            RuleFor(u => u.Senha)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(255);


            RuleFor(u => u.Tipo)
               .NotEmpty();


            RuleFor(u => u.Foto)
               .NotEmpty()
               .MinimumLength(5)
               .MaximumLength(510);

        }
    }
}

