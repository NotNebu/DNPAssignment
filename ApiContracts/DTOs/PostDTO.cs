namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere posts
    public class PostDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public int UserId { get; set; } 
    }
}