namespace Entities;

// Entity-klasse til at repræsentere et opslag
public class Post
{
    public Post()
    {
        // Constructor til at initialisere Entity Framework
    }
    
    // Constructor til at initialisere alle properties
    public Post(int id, string title, string body, int userId, User user, ICollection<Comment> comments)
    {
        Id = id;
        Title = title;
        Body = body;
        UserId = userId;
        User = user;
        Comments = comments;
    }

    // Properties til at tilgå og ændre på Post-entities
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
}