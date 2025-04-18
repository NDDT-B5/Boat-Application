using BoatApi.DTOs.Auth;
using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoatApi.Controllers;

/// <summary>
/// Controller that handles authentication-related actions, such as user login.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController(ITokenService tokenService, IUserService userService) : ControllerBase
{
    /// <summary>
    /// Authenticates a user by validating the provided username and password, and generates a JWT token.
    /// </summary>
    /// <param name="loginDto">The login data containing the username and password for authentication.</param>
    /// <returns>An <see cref="ActionResult"/> representing the outcome of the login attempt.</returns>
    /// <response code="200">Returns a token along with the user's role if login is successful.</response>
    /// <response code="401">Returns an unauthorized response if the username or password is invalid.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenAnswerDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var userDto = await userService.CheckIsValidUserAndPassword(loginDto);
        if (userDto == null)
            return Unauthorized("Invalid username or password.");

        return Ok(new TokenAnswerDto(tokenService.GenerateToken(userDto), userDto.Role));
    }
}