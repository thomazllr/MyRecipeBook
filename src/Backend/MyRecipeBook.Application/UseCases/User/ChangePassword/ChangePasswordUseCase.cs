using FluentValidation.Results;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Identity;
using MyRecipeBook.Domain.Security.PasswordHashing;
using MyRecipeBook.Exception;
using MyRecipeBook.Exception.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordHasher _passwordHasher;

    public ChangePasswordUseCase(
        ILoggedUser loggedUser, 
        IPasswordHasher passwordHasher)
    {
        _loggedUser = loggedUser;
        _passwordHasher = passwordHasher;
    }

    public Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = _loggedUser.Get();

        Validate(request, loggedUser);

    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User user)
    {
        var result = new ChangePasswordValidator().Validate(request);

        if (_passwordHasher.VerifyPassword(request.CurrentPassword, user.Password) == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.VALIDATION_CURRENT_PASSWORD));
        }

        if (result.IsValid == false)
        {
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
}}
