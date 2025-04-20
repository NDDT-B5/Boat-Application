using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BoatApi.DTOs.Auth;
using BoatApi.Services;
using Microsoft.Extensions.Configuration;

namespace BoatApi.Tests.Services;

public class TokenServiceTests
{
    private readonly TokenService _tokenService;

    public TokenServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "SuperSecretTestKey12345678901234567890"},
            {"Jwt:Issuer", "TestIssuer"}
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _tokenService = new TokenService(configuration);
    }

    [Fact]
    public void GenerateToken_ReturnsValidJwt_WithCorrectClaims()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var user = new UserDto(guid, "TestUser", "test@test.de", "1234", "User");

        // Act
        var tokenString = _tokenService.GenerateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenString);

        Assert.NotNull(token);
        Assert.Equal("TestIssuer", token.Issuer);

        var nameClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var idClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        Assert.Equal("TestUser", nameClaim);
        Assert.Equal(guid.ToString(), idClaim);
    }
}