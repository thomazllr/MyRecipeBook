using FluentValidation;
using MyRecipeBook.Communication.Requests;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserAccountValidator : AbstractValidator<RequestRegisterUserAccountJson>
{
    public RegisterUserAccountValidator()
    {
        RuleFor(user => user.Name).NotEmpty().WithMessage("Nome não pode ser vazio");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email não pode ser vazio");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Senha não pode ser vazio");
        When(user => string.IsNullOrEmpty(user.Email) == false, () =>
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("O email deve ser um email válido");
        });
    }
}
