namespace Entities;

// Entity-klasse til at repræsentere en bruger
public class User
{
    public User()
    {
        // Constructor til at initialisere Entity Framework
    }
    
    // Constructor til at initialisere alle properties
    public User(int id, string userName, string password, ICollection<Post> posts, ICollection<Comment> comments)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Posts = posts;
        Comments = comments;
    }

    // Properties til at tilgå og ændre på User-entities
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
}