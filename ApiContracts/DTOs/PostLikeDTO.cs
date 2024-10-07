namespace ApiContracts.DTOs
{
    public class PostLikeDTO
    {
        public int Id { get; set; } 
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}