using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KidsLearning.Backend.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KidsLearning.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var user = new IdentityUser
        {
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded) return Ok(new { Message = "User registered successfully." });

        return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
            return Unauthorized(new { Message = "Invalid login attempt." });

        var result =
            await _signInManager.PasswordSignInAsync(user.UserName ?? "", loginDto.Password, loginDto.RememberMe,
                false);

        if (!result.Succeeded)
            return Unauthorized(new { Message = "Invalid login attempt." });

        // JWT erstellen
        var token = GenerateJwtToken(user);

        return Ok(new
        {
            Token = token,
            Message = "Login successful."
        });
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Email ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}