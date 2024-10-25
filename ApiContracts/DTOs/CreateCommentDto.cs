namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere oprettelse af kommentarer
    public class CreateCommentDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
    }
}