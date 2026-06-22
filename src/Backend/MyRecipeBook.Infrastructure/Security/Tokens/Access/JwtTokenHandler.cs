using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Security.Tokens;
using System.Security.Claims;
using System.Text;

namespace MyRecipeBook.Infrastructure.Security.Tokens.Access;

internal sealed class JwtTokenHandler : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _singinKey;

    public JwtTokenHandler(uint expirationTimeMinutes, string singinKey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _singinKey = singinKey;
    }

    public string Generate(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(Credentials(), SecurityAlgorithms.HmacSha256),
            Subject = new ClaimsIdentity(claims)
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    private SymmetricSecurityKey Credentials()
    {
        var bytes = Encoding.UTF8.GetBytes(_singinKey);
        return new SymmetricSecurityKey(bytes);
    }
}
