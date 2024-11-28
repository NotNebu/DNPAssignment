using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle posts.
    /// </summary>
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;

        /// <summary>
        /// Constructor to initialize PostsController.
        /// </summary>
        /// <param name="postRepository">The repository for posts.</param>
        /// <param name="userRepository">The repository for users.</param>
        public PostsController(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Method to get all posts based on title and userId.
        /// </summary>
        /// <param name="title">Optional title to filter posts.</param>
        /// <param name="userId">Optional userId to filter posts.</param>
        /// <returns>A list of posts.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts(string? title = null, int? userId = null)
        {
            var posts = await _postRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(title))
            {
                posts = posts.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            if (userId.HasValue)
            {
                posts = posts.Where(p => p.UserId == userId.Value);
            }

            var users = await _userRepository.GetAllAsync();

            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId,
                Username = users.FirstOrDefault(u => u.Id == p.UserId)?.UserName ?? "Unknown"
            });

            return Ok(postDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(post.UserId);

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                Username = user?.UserName ?? "Unknown"
            };

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto request)
        {
            // Validate UserId and Username
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null || user.UserName != request.Username)
            {
                return BadRequest("Invalid user or username does not match.");
            }

            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                UserId = request.UserId
            };

            var createdPost = await _postRepository.AddAsync(post);

            var postDto = new PostDto
            {
                Id = createdPost.Id,
                Title = createdPost.Title,
                Body = createdPost.Body,
                UserId = createdPost.UserId,
                Username = user.UserName // Set the username here
            };

            return CreatedAtAction(nameof(GetPostById), new { id = postDto.Id }, postDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] CreatePostDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPost = await _postRepository.GetByIdAsync(id);

            if (existingPost == null)
            {
                return NotFound();
            }

            existingPost.Title = request.Title;
            existingPost.UserId = request.UserId;

            var updatedPost = await _postRepository.UpdateAsync(existingPost);
            if (updatedPost == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Method to delete a post.
        /// </summary>
        /// <param name="id">The Id of the post to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _postRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
