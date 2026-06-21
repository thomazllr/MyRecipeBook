using Bogus;
using CommonTestsUtilities.Security;
using MyRecipeBook.Domain.Entities;

namespace CommonTestsUtilities.Entities;

public class UserBuilder
{
    public static User Build()
    {
        return new Faker<User>()
            .RuleFor(user => user.Name, f => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, _ => GenerateRandomPassword());
    }

    private static string GenerateRandomPassword()
    {   
        var passwordEncripter = new IPasswordHasherBuilder().Build();

        var password = new Faker().Internet.Password();

        return passwordEncripter.HashPassword(password);
    }
}
