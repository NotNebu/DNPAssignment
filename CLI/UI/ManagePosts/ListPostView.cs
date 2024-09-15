using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class ListPostsView
    {
        private readonly IRepository<Post> _postRepository;

        public ListPostsView(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

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