using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository; 
        private readonly IRepository<Post> _postRepository; 
        
        public CommentsController(
            IRepository<Comment> commentRepository,
            IRepository<User> userRepository,
            IRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }
        
        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto request)
        {
            // Validate UserId
            var userExists = await _userRepository.GetByIdAsync(request.UserId);
            if (userExists == null)
            {
                return BadRequest($"User with ID {request.UserId} does not exist.");
            }

            // Validate PostId
            var postExists = await _postRepository.GetByIdAsync(request.PostId);
            if (postExists == null)
            {
                return BadRequest($"Post with ID {request.PostId} does not exist.");
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
                UserId = createdComment.UserId,
                UserName = userExists.UserName // Add username here
            };

            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }

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

            // Retrieve usernames by joining with the Users table
            var commentDtos = comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId,
                UserName = _userRepository.GetByIdAsync(c.UserId)?.Result?.UserName // Fetch username
            });

            return Ok(commentDtos);
        }
    }
}
