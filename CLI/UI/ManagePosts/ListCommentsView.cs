using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class ListCommentsView
    {
        private readonly IRepository<Comment> _commentRepository;

        public ListCommentsView(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task ListCommentsAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            foreach (var comment in comments)
            {
                Console.WriteLine($"ID: {comment.Id}, Post ID: {comment.PostId}, User ID: {comment.UserId}, Comment: {comment.Body}");
            }
        }
    }
}