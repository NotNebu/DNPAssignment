using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateUserDTO>>> GetAllUsers(string? username = null)
        {
            var users = await _userRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(username))
            {
                users = users.Where(u => u.UserName.Contains(username, StringComparison.OrdinalIgnoreCase));
            }
            
            var userDtos = users.Select(u => new CreateUserDTO
            {
                UserName = u.UserName,
                Password = u.Password
            });

            return Ok(userDtos);
        }
        
        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> AddUser([FromBody] CreateUserDTO request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password 
            };

            var createdUser = await _userRepository.AddAsync(user);
            
            var userDto = new CreateUserDTO
            {
                UserName = createdUser.UserName,
                Password = createdUser.Password
            };

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateUserDTO>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new CreateUserDTO
            {
                UserName = user.UserName,
                Password = user.Password 
            };

            return Ok(userDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDTO request)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            
            existingUser.UserName = request.UserName;
            existingUser.Password = request.Password;

            await _userRepository.UpdateAsync(existingUser);

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
