using BoatApi.DTOs.Auth;
using BoatApi.Models;
using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ITokenService tokenService, IUserService userService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
    {
        var userDto = await userService.CheckIsValidUserAndPassword(loginDto);
        if (userDto == null)
            return Unauthorized("Invalid username or password.");

        var token = tokenService.GenerateToken(userDto);

        return Ok(token);
    }
}