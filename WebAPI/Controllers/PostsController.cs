using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    // Controller til at håndtere posts
    [ApiController]
    
    // Route til at tilgå posts
    [Route("[controller]")]
    
    // PostController til at håndtere posts
    public class PostsController : ControllerBase
    {
        private readonly IRepository<Post> _postRepository;

        // Constructor til at initialisere PostController
        public PostsController(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
        
        // Metode til at hente alle posts baseret på titel og userId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts(string? title = null, int? userId = null)
        {
            // Henter alle posts
            var posts = await _postRepository.GetAllAsync();

            // Filtrer posts baseret på titel
            if (!string.IsNullOrEmpty(title))
            {
                posts = posts.Where(p => p.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            // Filtrer posts baseret på userId
            if (userId.HasValue)
            {
                posts = posts.Where(p => p.UserId == userId.Value);
            }
            
            // Omdanner posts til DTO'er
            var postDtos = posts.Select(p => new PostDTO
            {
                Id = p.Id,
                Title = p.Title,
                UserId = p.UserId
            });

            // Returnerer posts
            return Ok(postDtos);
        }
        
        // Metode til at hente en post baseret på dens Id
        [HttpGet("{id}")]
        
        // Metode til at hente en post baseret på dens Id
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
            // Henter post baseret på Id
            var post = await _postRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis post ikke findes
            if (post == null)
            {
                return NotFound();
            }

            // Omdanner post til DTO
            var postDto = new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                UserId = post.UserId
            };

            // Returnerer post
            return Ok(postDto);
        }
        
        // Metode til at oprette en post
        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostDTO request)
        {
            // Returnerer BadRequest hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Opretter en ny post
            var post = new Post
            {
                Title = request.Title,
                UserId = request.UserId
            };

            // Opretter post i databasen
            var createdPost = await _postRepository.AddAsync(post);

            // Omdanner post til en DTO
            var postDto = new PostDTO
            {
                Id = createdPost.Id,
                Title = createdPost.Title,
                UserId = createdPost.UserId
            };

            // Returnerer post
            return CreatedAtAction(nameof(GetPostById), new { id = postDto.Id }, postDto);
        }
        
        // Metode til at opdatere en post
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] CreatePostDTO request)
        {
            // Returnerer BadRequest hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Henter post baseret på Id
            var existingPost = await _postRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis post ikke findes
            if (existingPost == null)
            {
                return NotFound();
            }

            // Opdaterer post
            existingPost.Title = request.Title;
            existingPost.UserId = request.UserId;

            // Opdaterer post i databasen
            var updatedPost = await _postRepository.UpdateAsync(existingPost);
            if (updatedPost == null)
            {
                return NotFound();
            }
            
            // Returnerer opdateret post
            return NoContent();
        }
        
        // Metode til at slette en post
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            // Sletter post baseret på Id
            var deleted = await _postRepository.DeleteAsync(id);
            
            // Returnerer NotFound hvis post ikke findes 
            if (!deleted)
            {
                return NotFound();
            }

            // Returnerer NoContent hvis post er slettet
            return NoContent();
        }
    }
}
