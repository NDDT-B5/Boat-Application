using AutoMapper;
using BoatApi.Data;
using BoatApi.DTOs.Auth;
using BoatApi.Models;
using BoatApi.Services;
using BoatApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BoatApi.Tests.Services;

public class UserServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IPasswordService> _mockPasswordService;

    public UserServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
        });
        _mapper = config.CreateMapper();
        _mockPasswordService = new Mock<IPasswordService>();
    }

    private static ApplicationDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CheckIsValidUserAndPassword_UserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var context = CreateDbContext();
        var userService = new UserService(context, _mapper, _mockPasswordService.Object);

        var loginDto = new LoginDto("notFound", "pass");

        // Act
        var result = await userService.CheckIsValidUserAndPassword(loginDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckIsValidUserAndPassword_InvalidPassword_ReturnsNull()
    {
        // Arrange
        var context = CreateDbContext();
        context.Users.Add(new User("testUser", "test@example.com", "correctHash", "User"));
        await context.SaveChangesAsync();

        _mockPasswordService.Setup(p => p.HashPassword("wrongPass")).Returns("wrongHash");

        var userService = new UserService(context, _mapper, _mockPasswordService.Object);
        var loginDto = new LoginDto("testUser", "wrongPass");

        // Act
        var result = await userService.CheckIsValidUserAndPassword(loginDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CheckIsValidUserAndPassword_ValidCredentials_ReturnsUserDto()
    {
        // Arrange
        var context = CreateDbContext();
        var user = new User("testUser", "test@example.com", "correctHash", "User");

        context.Users.Add(user);
        await context.SaveChangesAsync();

        _mockPasswordService.Setup(p => p.HashPassword("password123")).Returns("correctHash");

        var userService = new UserService(context, _mapper, _mockPasswordService.Object);
        var loginDto =  new LoginDto("testUser", "password123");

        // Act
        var result = await userService.CheckIsValidUserAndPassword(loginDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Username, result.Username);
    }
}