using CommonTestsUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;

namespace UseCases.Tests.User.Register;

public class RegisterUserAccountUseCaseTests
{

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserAccountJsonBuilder.Build();

        var useCase = CreateUseCase();

        await useCase.Execute(request);
    }


    private RegisterUserAccountUseCase CreateUseCase()
    {
        return new RegisterUserAccountUseCase(null, null, null, null);
    }
}
