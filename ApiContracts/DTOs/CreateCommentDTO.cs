namespace ApiContracts.DTOs
{
    public class CreateCommentDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
    }
}