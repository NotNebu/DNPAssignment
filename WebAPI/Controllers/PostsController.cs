using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;

        public PostsController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts(string? title = null, int? userId = null)
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
            
            var postDtos = posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                UserId = p.UserId
            });

            return Ok(postDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                UserId = post.UserId
            };

            return Ok(postDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = new Post
            {
                Title = request.Title,
                UserId = request.UserId
            };

            var createdPost = await _postRepository.AddAsync(post);

            var postDto = new PostDTO
            {
                Id = createdPost.Id,
                Title = createdPost.Title,
                UserId = createdPost.UserId
            };

            return CreatedAtAction(nameof(GetPostById), new { id = postDto.Id }, postDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] CreatePostDTO request)
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
