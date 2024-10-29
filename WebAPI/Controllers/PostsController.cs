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

        /// <summary>
        /// Constructor to initialize PostsController.
        /// </summary>
        /// <param name="postRepository">The repository for posts.</param>
        public PostsController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
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
            
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Body = p.Body,
                UserId = p.UserId
            });

            return Ok(postDtos);
        }
        
        /// <summary>
        /// Method to get a post by its Id.
        /// </summary>
        /// <param name="id">The Id of the post.</param>
        /// <returns>The post with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            
            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId
            };

            return Ok(postDto);
        }
        
        /// <summary>
        /// Method to create a new post.
        /// </summary>
        /// <param name="request">The request containing the post details.</param>
        /// <returns>The created post.</returns>
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                UserId = createdPost.UserId
            };

            return CreatedAtAction(nameof(GetPostById), new { id = postDto.Id }, postDto);
        }
        
        /// <summary>
        /// Method to update an existing post.
        /// </summary>
        /// <param name="id">The Id of the post to update.</param>
        /// <param name="request">The request containing the updated post details.</param>
        /// <returns>No content if the update is successful.</returns>
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