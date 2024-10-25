namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere posts
    public class PostDto
    {
        public int Id { get; set; } 
        public string Body { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; } 
    }
}