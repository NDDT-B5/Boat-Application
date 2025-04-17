using BoatApi.DTOs.Auth;
using BoatApi.Models;
using BoatApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController(ITokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto login)
    {


        return Unauthorized();
    }
}