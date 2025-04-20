using BoatApi.Services;
using BoatApi.Services.Interfaces;

namespace BoatApi.Tests.Services;

public class PasswordServiceTests
{
    private readonly IPasswordService _passwordService = new PasswordService();

    [Fact]
    public void HashPassword_ShouldReturnHashedString()
    {
        // Arrange
        const string password = "SuperSecret123!";

        // Act
        var hashed = _passwordService.HashPassword(password);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(hashed));
    }

    [Fact]
    public void HashPassword_SameInput_ShouldReturnSameHash()
    {
        // Arrange
        const string password = "SuperSecret123!";

        // Act
        var hash1 = _passwordService.HashPassword(password);
        var hash2 = _passwordService.HashPassword(password);

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void HashPassword_DifferentInputs_ShouldReturnDifferentHashes()
    {
        // Arrange
        const string password1 = "Password1!";
        const string password2 = "Password2!";

        // Act
        var hash1 = _passwordService.HashPassword(password1);
        var hash2 = _passwordService.HashPassword(password2);

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}