namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere likes på posts
    public class PostLikeDto
    {
        public int Id { get; set; } 
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}