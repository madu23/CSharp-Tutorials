using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RtlEmployeeApi.Models.Entities;
using RtlEmployeeApi.Models.service;
using RtlEmployeeApi.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto loginDto)
    {
        if (loginDto.Username == "admin" && loginDto.Password == "password123")
        {
            var token = _tokenService.GenerateToken(loginDto.Username, "Admin");
            return Ok(new { token });
        }

        return Unauthorized("Invalid credentials");
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               