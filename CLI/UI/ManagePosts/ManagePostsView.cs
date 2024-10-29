using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    /// <summary>
    /// Provides methods to manage posts in the CLI application.
    /// </summary>
    public class ManagePostsView
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly AddCommentView _addCommentView;
        private readonly ListCommentsView _listCommentsView;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagePostsView"/> class.
        /// </summary>
        /// <param name="postRepository">The repository for posts.</param>
        /// <param name="commentRepository">The repository for comments.</param>
        /// <param name="userRepository">The repository for users.</param>
        public ManagePostsView(IRepository<Post> postRepository, IRepository<Comment> commentRepository, IRepository<User> userRepository)
        {
            _createPostView = new CreatePostView(postRepository, userRepository); 
            _listPostsView = new ListPostsView(postRepository);
            _addCommentView = new AddCommentView(commentRepository, postRepository, userRepository);
            _listCommentsView = new ListCommentsView(commentRepository);
        }

        /// <summary>
        /// Prompts the user to create a new post asynchronously.
        /// </summary>
        public async Task CreatePostAsync()
        {
            await _createPostView.CreatePostAsync();
        }

        /// <summary>
        /// Lists all posts asynchronously.
        /// </summary>
        public async Task ListPostsAsync()
        {
            await _listPostsView.ListPostsAsync();
        }

        /// <summary>
        /// Prompts the user to add a comment to a post asynchronously.
        /// </summary>
        public async Task AddCommentAsync()
        {
            await _addCommentView.AddCommentAsync();
        }

        /// <summary>
        /// Lists all comments asynchronously.
        /// </summary>
        public async Task ListCommentsAsync()
        {
            await _listCommentsView.ListCommentsAsync();
        }
    }
}