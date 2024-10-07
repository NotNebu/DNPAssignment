using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentsController(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments(int? userId = null, int? postId = null)
        {
            var comments = await _commentRepository.GetAllAsync();

            if (userId.HasValue)
            {
                comments = comments.Where(c => c.UserId == userId.Value);
            }

            if (postId.HasValue)
            {
                comments = comments.Where(c => c.PostId == postId.Value);
            }
            
            var commentDtos = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId
            });

            return Ok(commentDtos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = new CommentDTO
            {
                Id = comment.Id,
                Body = comment.Body,
                PostId = comment.PostId,
                UserId = comment.UserId
            };

            return Ok(commentDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new Comment
            {
                Body = request.Body,
                PostId = request.PostId,
                UserId = request.UserId
            };

            var createdComment = await _commentRepository.AddAsync(comment);

            var commentDto = new CommentDTO
            {
                Id = createdComment.Id,
                Body = createdComment.Body,
                PostId = createdComment.PostId,
                UserId = createdComment.UserId
            };

            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CreateCommentDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingComment = await _commentRepository.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Body = request.Body;
            existingComment.PostId = request.PostId;
            existingComment.UserId = request.UserId;

            var updatedComment = await _commentRepository.UpdateAsync(existingComment);
            if (updatedComment == null)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var deleted = await _commentRepository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
