namespace Entities;

public class User
{
    public User(int id, string userName, string password, ICollection<Post> posts, ICollection<Comment> comments)
    {
        Id = id;
        UserName = userName;
        Password = password;
        Posts = posts;
        Comments = comments;
    }

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
}