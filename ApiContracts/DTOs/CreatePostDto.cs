namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere oprettelse af posts
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}