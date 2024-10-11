namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere oprettelse af posts
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public int UserId { get; set; }
    }
}