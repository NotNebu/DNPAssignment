using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    /// <summary>
    /// Provides methods to list posts in the CLI application.
    /// </summary>
    public class ListPostsView
    {
        private readonly IRepository<Post> _postRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListPostsView"/> class.
        /// </summary>
        /// <param name="postRepository">The repository for posts.</param>
        public ListPostsView(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        /// <summary>
        /// Lists all posts asynchronously.
        /// </summary>
        public async Task ListPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}, Title: {post.Title}, User ID: {post.UserId}");
            }
        }
    }
}