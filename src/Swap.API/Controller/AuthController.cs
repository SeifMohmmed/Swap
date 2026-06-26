using global::Swap.API.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Swap.API.Controller;


[ApiController]
[Route("api/[controller]")]
public class AuthController(ApplicationDbContext context) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var userExists = await context.Users
            .AnyAsync(u => u.UserName.ToLower() == request.Username.ToLower());

        if (userExists)
        {
            return BadRequest(new { message = "Username is already taken." });
        }
        var passwordBytes = System.Text.Encoding.UTF8.GetBytes(request.Password);
        string hashedPassword = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(passwordBytes));

        var newUser = new Users
        {
            UserName = request.Username,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = hashedPassword
        };

        context.Users.Add(newUser);
        await context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully!" });
    }
}
