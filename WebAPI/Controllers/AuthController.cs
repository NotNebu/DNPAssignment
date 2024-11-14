using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IRepository<User> _userRepository;

    public AuthController(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username))
        {
            return BadRequest("Invalid login request.");
        }

        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.UserName.Equals(loginRequest.Username, StringComparison.OrdinalIgnoreCase));

        if (user == null || user.Password != loginRequest.Password)
        {
            return Unauthorized("Invalid username or password.");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.UserName
        };

        return Ok(userDto);
    }
}