using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to handle comments.
    /// </summary>
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentRepository;

        /// <summary>
        /// Constructor to initialize CommentsController.
        /// </summary>
        /// <param name="commentRepository">The repository for comments.</param>
        public CommentsController(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        /// <summary>
        /// Method to get all comments based on userId and postId.
        /// </summary>
        /// <param name="userId">Optional userId to filter comments.</param>
        /// <param name="postId">Optional postId to filter comments.</param>
        /// <returns>A list of comments.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetAllComments(int? userId = null, int? postId = null)
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
            
            var commentDtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId
            });

            return Ok(commentDtos);
        }
        
        /// <summary>
        /// Method to get a comment by its Id.
        /// </summary>
        /// <param name="id">The Id of the comment.</param>
        /// <returns>The comment with the specified Id.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                Body = comment.Body,
                PostId = comment.PostId,
                UserId = comment.UserId
            };

            return Ok(commentDto);
        }
        
        /// <summary>
        /// Method to create a new comment.
        /// </summary>
        /// <param name="request">The request containing the comment details.</param>
        /// <returns>The created comment.</returns>
        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto request)
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

            var commentDto = new CommentDto
            {
                Id = createdComment.Id,
                Body = createdComment.Body,
                PostId = createdComment.PostId,
                UserId = createdComment.UserId
            };

            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }
        
        /// <summary>
        /// Method to update an existing comment.
        /// </summary>
        /// <param name="id">The Id of the comment to update.</param>
        /// <param name="request">The request containing the updated comment details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CreateCommentDto request)
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
        
        /// <summary>
        /// Method to delete a comment.
        /// </summary>
        /// <param name="id">The Id of the comment to delete.</param>
        /// <returns>No content if the deletion is successful.</returns>
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