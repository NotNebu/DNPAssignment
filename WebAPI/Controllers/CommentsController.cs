using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using Entities;
using ApiContracts.DTOs;

namespace WebAPI.Controllers
{
    // Controller til at håndtere kommentarer
    [ApiController]
    
    // Route til at tilgå kommentarer
    [Route("[controller]")]
    
    // Kommentarcontroller til at håndtere kommentarer
    public class CommentsController : ControllerBase
    {
        private readonly IRepository<Comment> _commentRepository;

        // Constructor til at initialisere CommentController
        public CommentsController(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }
        
        // Metode til at hente alle kommentarer baseret på userId og postId
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments(int? userId = null, int? postId = null)
        {
            // Henter alle kommentarer
            var comments = await _commentRepository.GetAllAsync();

            // Filtrerer kommentarer baseret på userId
            if (userId.HasValue)
            {
                // Filtrer kommentarer baseret på userId
                comments = comments.Where(c => c.UserId == userId.Value);
            }

            // Filtrer kommentarer baseret på postId
            if (postId.HasValue)
            {
                // Filtrer kommentarer baseret på postId
                comments = comments.Where(c => c.PostId == postId.Value);
            }
            
            // Omdanner kommentarer til DTO'er
            var commentDtos = comments.Select(c => new CommentDTO
            {
                Id = c.Id,
                Body = c.Body,
                PostId = c.PostId,
                UserId = c.UserId
            });

            // Returnerer kommentarerne
            return Ok(commentDtos);
        }
        
        // Metode til at hente en kommentar baseret på dens Id
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
        {
            // Henter kommentar baseret på Id
            var comment = await _commentRepository.GetByIdAsync(id);
            
            // Returnerer NotFound, hvis kommentaren ikke findes
            if (comment == null)
            {
                return NotFound();
            }

            // Omdanner kommentaren til en DTO
            var commentDto = new CommentDTO
            {
                Id = comment.Id,
                Body = comment.Body,
                PostId = comment.PostId,
                UserId = comment.UserId
            };

            // Returnerer kommentaren
            return Ok(commentDto);
        }
        
        // Metode til at oprette en kommentar
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] CreateCommentDTO request)
        {
            // Returnerer BadRequest, hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Opretter en ny kommentar
            var comment = new Comment
            {
                Body = request.Body,
                PostId = request.PostId,
                UserId = request.UserId
            };

            // Tilføjer den nye kommentar til databasen
            var createdComment = await _commentRepository.AddAsync(comment);

            // Omdanner den nye kommentar til en DTO
            var commentDto = new CommentDTO
            {
                Id = createdComment.Id,
                Body = createdComment.Body,
                PostId = createdComment.PostId,
                UserId = createdComment.UserId
            };

            // Returnerer den nye kommentar
            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }
        
        // Metode til at opdatere en kommentar
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CreateCommentDTO request)
        {
            // Returnerer BadRequest, hvis modelstate ikke er valid
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Henter eksisterende kommentar baseret på Id
            var existingComment = await _commentRepository.GetByIdAsync(id);
            
            // Returnerer NotFound, hvis kommentaren ikke findes
            if (existingComment == null)
            {
                return NotFound();
            }

            // Opdaterer kommentaren
            existingComment.Body = request.Body;
            existingComment.PostId = request.PostId;
            existingComment.UserId = request.UserId;

            // Opdaterer kommentaren i databasen
            var updatedComment = await _commentRepository.UpdateAsync(existingComment);
            
            // Returnerer NotFound, hvis kommentaren ikke findes
            if (updatedComment == null)
            {
                return NotFound();
            }

            // Returnerer den opdaterede kommentar
            return NoContent();
        }
        
        // Metode til at slette en kommentar
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            // Sletter kommentar baseret på Id
            var deleted = await _commentRepository.DeleteAsync(id);
            
            // Returnerer NotFound, hvis kommentaren ikke findes
            if (!deleted)
            {
                return NotFound();
            }

            // Returnerer NoContent, hvis kommentaren er slettet
            return NoContent();
        }
    }
}
