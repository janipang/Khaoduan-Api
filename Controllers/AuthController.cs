using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using khaoduan_api.Models;
using khaoduan_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt; // Alias for clarity

namespace khaoduan_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IDatabaseService _db;
    private readonly IConfiguration _config;

    public AuthController(IDatabaseService db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var account = await _db.GetAccountAsync(request.Username);
        if (account is not null && BC.Verify(request.Password, account.Password))
        {
            var token = GenerateToken(request.Username);
            return Ok(new
            {
                username = request.Username,
                token
            });
        }

        return Unauthorized(new { message = "Invalid username or password" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest request)
    {
        var existing = await _db.GetAccountAsync(request.Username);
        if (existing != null)
        {
            return BadRequest(new { message = "Username already exists" });
        }
        

        string hashedPassword = BC.HashPassword(request.Password);
        var account = new Account
        {
            Username = request.Username,
            Password = hashedPassword,
            Status = "active"
        };

        await _db.CreateAccountAsync(account);

        var token = GenerateToken(request.Username);

        return Ok(new
        {
            username = request.Username,
            accesstoken = token
        });
    }

    private string GenerateToken(string username)
    {
        var jwtConfig = _config.GetSection("JwtConfig");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
        var audiences = _config.GetSection("JwtConfig:Audiences").Get<string[]>()!;

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: audiences[0],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtConfig["TokenValidityMins"]!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record LoginRequest(string Username, string Password);