using CommonTestsUtilities.Repositories;
using CommonTestsUtilities.Requests;
using CommonTestsUtilities.Security;
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
        var unitOfWork = IUnityOfWorkBuilder.Build();
        var userWriteOnlyRepository = IUserWriteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new IUserReadOnlyRepositoryBuilder().Build();
        var passwordHasher = new IPasswordHasherBuilder().Build();

        return new RegisterUserAccountUseCase(passwordHasher, userWriteOnlyRepository, userReadOnlyRepository, unitOfWork);
    }
}
