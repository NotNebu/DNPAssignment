using RepositoryContracts;
using Entities;

namespace CLI.UI;

public class CliApp
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Post> _postRepository;
    private readonly IRepository<Comment> _commentRepository;

    public CliApp(IRepository<User> userRepository, IRepository<Post> postRepository, IRepository<Comment> commentRepository)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("1: Create User");
            Console.WriteLine("2: Create Post");
            Console.WriteLine("3: Add Comment");
            Console.WriteLine("4: View All Users");
            Console.WriteLine("5: View All Posts");
            Console.WriteLine("6: View All Comments");
            Console.WriteLine("7: Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await CreateUserAsync();
                    break;
                case "2":
                    await CreatePostAsync();
                    break;
                case "3":
                    await AddCommentAsync();
                    break;
                case "4":
                    await ViewUsersAsync();
                    break;
                case "5":
                    await ViewPostsAsync();
                    break;
                case "6":
                    await ViewCommentsAsync();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }
    }
    
    private async Task CreateUserAsync()
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        var user = new User { UserName = username, Password = password };
        await _userRepository.AddAsync(user);
        Console.WriteLine("User created successfully.");
    }
    
    private async Task CreatePostAsync()
    {
        Console.Write("Enter post title: ");
        var title = Console.ReadLine();
        Console.Write("Enter post body: ");
        var body = Console.ReadLine();
        Console.Write("Enter user ID: ");
        var userId = int.Parse(Console.ReadLine());

        var post = new Post { Title = title, Body = body, UserId = userId };
        await _postRepository.AddAsync(post);
        Console.WriteLine("Post created successfully.");
    }
    
    private async Task AddCommentAsync()
    {
        Console.Write("Enter post ID: ");
        var postId = int.Parse(Console.ReadLine());
        Console.Write("Enter comment body: ");
        var body = Console.ReadLine();
        Console.Write("Enter user ID: ");
        var userId = int.Parse(Console.ReadLine());

        var comment = new Comment { PostId = postId, Body = body, UserId = userId };
        await _commentRepository.AddAsync(comment);
        Console.WriteLine("Comment added successfully.");
    }
    
    private async Task ViewUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.UserName}");
        }
    }
    
    private async Task ViewPostsAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        foreach (var post in posts)
        {
            Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, User ID: {post.UserId}");
        }
    }

    private async Task ViewCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        foreach (var comment in comments)
        {
            Console.WriteLine($"ID: {comment.Id}, Post ID: {comment.PostId}, User ID: {comment.UserId}, Comment: {comment.Body}");
        }
    }
}
