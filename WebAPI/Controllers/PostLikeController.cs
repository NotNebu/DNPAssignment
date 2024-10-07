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
    public class PostLikesController : ControllerBase
    {
        private readonly IRepository<PostLike> _postLikeRepository;

        public PostLikesController(IRepository<PostLike> postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostLikeDTO>>> GetAllPostLikes()
        {
            var postLikes = await _postLikeRepository.GetAllAsync();

            var postLikeDtos = postLikes.Select(pl => new PostLikeDTO
            {
                Id = pl.Id,
                PostId = pl.PostId,
                UserId = pl.UserId
            });

            return Ok(postLikeDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<PostLikeDTO>> GetPostLikeById(int id)
        {
            var postLike = await _postLikeRepository.GetByIdAsync(id);
            if (postLike == null)
            {
                return NotFound();
            }

            var postLikeDto = new PostLikeDTO
            {
                Id = postLike.Id,
                PostId = postLike.PostId,
                UserId = postLike.UserId
            };

            return Ok(postLikeDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<PostLikeDTO>> CreatePostLike([FromBody] PostLikeDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postLike = new PostLike
            {
                PostId = request.PostId,
                UserId = request.UserId
            };

            var createdPostLike = await _postLikeRepository.AddAsync(postLike);

            var postLikeDto = new PostLikeDTO
            {
                Id = createdPostLike.Id,
                PostId = createdPostLike.PostId,
                UserId = createdPostLike.UserId
            };

            return CreatedAtAction(nameof(GetPostLikeById), new { id = postLikeDto.Id }, postLikeDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostLike(int id, [FromBody] PostLikeDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPostLike = await _postLikeRepository.GetByIdAsync(id);
            if (existingPostLike == null)
            {
                return NotFound();
            }

            existingPostLike.PostId = request.PostId;
            existingPostLike.UserId = request.UserId;

            var updatedPostLike = await _postLikeRepository.UpdateAsync(existingPostLike);
            if (updatedPostLike == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostLike(int id)
        {
            var deleted = await _postLikeRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
