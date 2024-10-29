using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    /// <summary>
    /// Provides methods to list comments in the CLI application.
    /// </summary>
    public class ListCommentsView
    {
        private readonly IRepository<Comment> _commentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommentsView"/> class.
        /// </summary>
        /// <param name="commentRepository">The repository for comments.</param>
        public ListCommentsView(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Lists all comments asynchronously.
        /// </summary>
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