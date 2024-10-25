using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    // Controller til at håndtere brugere
    [ApiController]
    
    // Route til at tilgå brugere
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        // Constructor til at initialisere UserController
        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        // Metode til at hente alle brugere baseret på brugernavn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateUserDto>>> GetAllUsers(string? username = null)
        {
            // Henter alle brugere
            var users = await _userRepository.GetAllAsync();

            // Filtrer brugere baseret på brugernavn
            if (!string.IsNullOrEmpty(username))
            {
                users = users.Where(u => u.UserName.Contains(username, StringComparison.OrdinalIgnoreCase));
            }
            
            // Omdanner brugere til DTO'er
            var userDtos = users.Select(u => new CreateUserDto
            {
                UserName = u.UserName,
                Password = u.Password
            });

            // Returnerer brugere
            return Ok(userDtos);
        }
        
        // Metode til at oprette en bruger
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> AddUser([FromBody] CreateUserDto request)
        {
            // Opretter en bruger
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password 
            };

            // Tilføjer brugeren til databasen
            var createdUser = await _userRepository.AddAsync(user);
            
            // Omdanner brugeren til en DTO
            var userDto = new CreateUserDto
            {
                UserName = createdUser.UserName,
                Password = createdUser.Password
            };

            // Returnerer brugeren
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
        }
        
        // Metode til at hente en bruger baseret på dens Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateUserDto>> GetUserById(int id)
        {
            // Henter bruger baseret på Id
            var user = await _userRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis bruger ikke findes
            if (user == null)
            {
                return NotFound();
            }

            // Omdanner bruger til DTO
            var userDto = new CreateUserDto
            {
                UserName = user.UserName,
                Password = user.Password 
            };

            // Returnerer bruger
            return Ok(userDto);
        }
        
        // Metode til at opdatere en bruger
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto request)
        {
            // Henter bruger baseret på Id
            var existingUser = await _userRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis bruger ikke findes
            if (existingUser == null)
            {
                return NotFound();
            }
            
            // Opdaterer brugerens properties
            existingUser.UserName = request.UserName;
            existingUser.Password = request.Password;

            // Opdaterer bruger i databasen
            await _userRepository.UpdateAsync(existingUser);

            // Returnerer bruger
            return NoContent();
        }
        
        // Metode til at slette en bruger
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Sletter bruger baseret på Id
            var deleted = await _userRepository.DeleteAsync(id);
            
            // Returnerer NotFound hvis bruger ikke findes
            if (!deleted)
            {
                return NotFound();
            }

            // Returnerer bruger
            return NoContent();
        }
    }
}
