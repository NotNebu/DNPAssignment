using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle users.
    /// </summary>
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Constructor to initialize UsersController.
        /// </summary>
        /// <param name="userRepository">The repository for users.</param>
        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        /// <summary>
        /// Method to get all users based on username.
        /// </summary>
        /// <param name="username">Optional username to filter users.</param>
        /// <returns>A list of users.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreateUserDto>>> GetAllUsers(string? username = null)
        {
            var users = await _userRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(username))
            {
                users = users.Where(u => u.UserName.Contains(username, StringComparison.OrdinalIgnoreCase));
            }
            
            var userDtos = users.Select(u => new CreateUserDto
            {
                UserName = u.UserName,
                Password = u.Password
            });

            return Ok(userDtos);
        }
        
        /// <summary>
        /// Method to create a user.
        /// </summary>
        /// <param name="request">The request containing the user details.</param>
        /// <returns>The created user.</returns>
        [HttpPost]
        public async Task<ActionResult<CreateUserDto>> AddUser([FromBody] CreateUserDto request)
        {
            var user = new User
            {
                UserName = request.UserName,
                Password = request.Password 
            };

            var createdUser = await _userRepository.AddAsync(user);
            
            var userDto = new CreateUserDto
            {
                UserName = createdUser.UserName,
                Password = createdUser.Password
            };

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userDto);
        }
        
        /// <summary>
        /// Method to get a user by its Id.
        /// </summary>
        /// <param name="id">The Id of the user.</param>
        /// <returns>The user with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateUserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new CreateUserDto
            {
                UserName = user.UserName,
                Password = user.Password 
            };

            return Ok(userDto);
        }
        
        /// <summary>
        /// Method to update an existing user.
        /// </summary>
        /// <param name="id">The Id of the user to update.</param>
        /// <param name="request">The request containing the updated user details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDto request)
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
        
        /// <summary>
        /// Method to delete a user.
        /// </summary>
        /// <param name="id">The Id of the user to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
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