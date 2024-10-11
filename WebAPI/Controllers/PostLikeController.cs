using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    // Controller til at håndtere post likes
    [ApiController]
    
    // Route til at tilgå post likes
    [Route("[controller]")]
    
    // PostLikeController til at håndtere post likes
    public class PostLikesController : ControllerBase
    {
        private readonly IRepository<PostLike> _postLikeRepository;

        // Constructor til at initialisere PostLikeController
        public PostLikesController(IRepository<PostLike> postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }
        
        // Metode til at hente alle post likes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostLikeDTO>>> GetAllPostLikes()
        {
            // Henter alle post likes
            var postLikes = await _postLikeRepository.GetAllAsync();

            // Omdanner post likes til DTO'er
            var postLikeDtos = postLikes.Select(pl => new PostLikeDTO
            {
                Id = pl.Id,
                PostId = pl.PostId,
                UserId = pl.UserId
            });

            // Returnerer post likes
            return Ok(postLikeDtos);
        }
        
        // Metode til at hente et post like baseret på dets Id
        [HttpGet("{id}")]
        public async Task<ActionResult<PostLikeDTO>> GetPostLikeById(int id)
        {
            // Henter post like baseret på Id
            var postLike = await _postLikeRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis post like ikke findes
            if (postLike == null)
            {
                return NotFound();
            }

            // Omdanner post like til DTO
            var postLikeDto = new PostLikeDTO
            {
                Id = postLike.Id,
                PostId = postLike.PostId,
                UserId = postLike.UserId
            };

            // Returnerer post like
            return Ok(postLikeDto);
        }
        
        // Metode til at oprette et post like
        [HttpPost]
        public async Task<ActionResult<PostLikeDTO>> CreatePostLike([FromBody] PostLikeDTO request)
        {
            // Returnerer BadRequest hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                // Returnerer BadRequest
                return BadRequest(ModelState);
            }

            // Opretter et nyt post like
            var postLike = new PostLike
            {
                PostId = request.PostId,
                UserId = request.UserId
            };

            // Opretter post like i databasen
            var createdPostLike = await _postLikeRepository.AddAsync(postLike);

            // Omdanner post like til en DTO
            var postLikeDto = new PostLikeDTO
            {
                Id = createdPostLike.Id,
                PostId = createdPostLike.PostId,
                UserId = createdPostLike.UserId
            };

            // Returnerer det nye post like
            return CreatedAtAction(nameof(GetPostLikeById), new { id = postLikeDto.Id }, postLikeDto);
        }
        
        // Metode til at opdatere et post like
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostLike(int id, [FromBody] PostLikeDTO request)
        {
            // Returnerer BadRequest hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Henter post like baseret på Id
            var existingPostLike = await _postLikeRepository.GetByIdAsync(id);
            
            // Returnerer NotFound hvis post like ikke findes
            if (existingPostLike == null)
            {
                return NotFound();
            }

            // Opdaterer post like
            existingPostLike.PostId = request.PostId;
            existingPostLike.UserId = request.UserId;

            // Opdaterer post like i databasen
            var updatedPostLike = await _postLikeRepository.UpdateAsync(existingPostLike);
            
            // Returnerer NotFound hvis post like ikke findes
            if (updatedPostLike == null)
            {
                return NotFound();
            }

            // Returnerer det opdaterede post like
            return NoContent();
        }
        
        // Metode til at slette et post like
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostLike(int id)
        {
            // Sletter post like baseret på Id
            var deleted = await _postLikeRepository.DeleteAsync(id);
            
            // Returnerer NotFound hvis post like ikke findes
            if (!deleted)
            {
                return NotFound();
            }

            // Returnerer NoContent hvis post like er slettet
            return NoContent();
        }
    }
}
