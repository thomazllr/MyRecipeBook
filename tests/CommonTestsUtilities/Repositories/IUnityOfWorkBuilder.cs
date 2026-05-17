using Moq;
using MyRecipeBook.Domain.Repositories;

namespace CommonTestsUtilities.Repositories;

public class IUnityOfWorkBuilder
{

    public static IUnityOfWork Build()
    {
        var moq = new Mock<IUnityOfWork>();

        return moq.Object;
    }

}
