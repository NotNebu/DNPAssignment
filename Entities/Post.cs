namespace Entities;

public class Post
{
    public Post(int id, string title, string body, int userId, User user, ICollection<Comment> comments)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
        User = user;
        Comments = comments;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    
}