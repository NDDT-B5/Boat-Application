using BoatApi.Controllers;
using BoatApi.DTOs.Auth;
using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BoatApi.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<ITokenService> _mockTokenService = new();
    private readonly Mock<IUserService> _mockUserService = new();
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _controller = new AuthController(_mockTokenService.Object, _mockUserService.Object);
    }

    [Fact]
    public async Task Login_InvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginDto = new LoginDto("user", "wrongPassword");
        _mockUserService.Setup(s => s.CheckIsValidUserAndPassword(loginDto))
                        .ReturnsAsync((UserDto?)null);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var unauthorized = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.Equal("Invalid username or password.", unauthorized.Value);

        _mockUserService.Verify(s => s.CheckIsValidUserAndPassword(loginDto), Times.Once);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsTokenAndRole()
    {
        // Arrange
        var loginDto = new LoginDto("user", "correctPassword");
        var userDto = new UserDto(Guid.NewGuid(), "user", "Admin", "1234", "User");
        var fakeToken = "fake-jwt-token";

        _mockUserService.Setup(s => s.CheckIsValidUserAndPassword(loginDto))
                        .ReturnsAsync(userDto);
        _mockTokenService.Setup(s => s.GenerateToken(userDto))
                         .Returns(fakeToken);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var tokenAnswer = Assert.IsType<TokenAnswerDto>(okResult.Value);

        Assert.Equal(fakeToken, tokenAnswer.JwtToken);
        Assert.Equal(userDto.Role, tokenAnswer.Role);

        _mockUserService.Verify(s => s.CheckIsValidUserAndPassword(loginDto), Times.Once);
        _mockTokenService.Verify(s => s.GenerateToken(userDto), Times.Once);
    }
}
