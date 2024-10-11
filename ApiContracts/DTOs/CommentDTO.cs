namespace ApiContracts.DTOs
{
    // Data Transfer Object til at håndtere kommentarer
    public class CommentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
    }
}