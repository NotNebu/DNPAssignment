using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle post likes.
    /// </summary>
    [ApiController]
    [Route("api/postlikes")]
    public class PostLikesController : ControllerBase
    {
        private readonly IRepository<PostLike> _postLikeRepository;

        /// <summary>
        /// Constructor to initialize PostLikesController.
        /// </summary>
        /// <param name="postLikeRepository">The repository for post likes.</param>
        public PostLikesController(IRepository<PostLike> postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }
        
        /// <summary>
        /// Method to get all post likes.
        /// </summary>
        /// <returns>A list of post likes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostLikeDto>>> GetAllPostLikes()
        {
            var postLikes = await _postLikeRepository.GetAllAsync();

            var postLikeDtos = postLikes.Select(pl => new PostLikeDto
            {
                Id = pl.Id,
                PostId = pl.PostId,
                UserId = pl.UserId
            });

            return Ok(postLikeDtos);
        }
        
        /// <summary>
        /// Method to get a post like by its Id.
        /// </summary>
        /// <param name="id">The Id of the post like.</param>
        /// <returns>The post like with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostLikeDto>> GetPostLikeById(int id)
        {
            var postLike = await _postLikeRepository.GetByIdAsync(id);
            
            if (postLike == null)
            {
                return NotFound();
            }

            var postLikeDto = new PostLikeDto
            {
                Id = postLike.Id,
                PostId = postLike.PostId,
                UserId = postLike.UserId
            };

            return Ok(postLikeDto);
        }
        
        /// <summary>
        /// Method to create a new post like.
        /// </summary>
        /// <param name="request">The request containing the post like details.</param>
        /// <returns>The created post like.</returns>
        [HttpPost]
        public async Task<ActionResult<PostLikeDto>> CreatePostLike([FromBody] PostLikeDto request)
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

            var postLikeDto = new PostLikeDto
            {
                Id = createdPostLike.Id,
                PostId = createdPostLike.PostId,
                UserId = createdPostLike.UserId
            };

            return CreatedAtAction(nameof(GetPostLikeById), new { id = postLikeDto.Id }, postLikeDto);
        }
        
        /// <summary>
        /// Method to update an existing post like.
        /// </summary>
        /// <param name="id">The Id of the post like to update.</param>
        /// <param name="request">The request containing the updated post like details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePostLike(int id, [FromBody] PostLikeDto request)
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
        
        /// <summary>
        /// Method to delete a post like.
        /// </summary>
        /// <param name="id">The Id of the post like to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
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