using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts
{
    public class ManagePostsView
    {
        private readonly CreatePostView _createPostView;
        private readonly ListPostsView _listPostsView;
        private readonly AddCommentView _addCommentView;
        private readonly ListCommentsView _listCommentsView;

        public ManagePostsView(IRepository<Post> postRepository, IRepository<Comment> commentRepository, IRepository<User> userRepository)
        {
            _createPostView = new CreatePostView(postRepository, userRepository); 
            _listPostsView = new ListPostsView(postRepository);
            _addCommentView = new AddCommentView(commentRepository, postRepository, userRepository);
            _listCommentsView = new ListCommentsView(commentRepository);
        }

        public async Task CreatePostAsync()
        {
            await _createPostView.CreatePostAsync();
        }

        public async Task ListPostsAsync()
        {
            await _listPostsView.ListPostsAsync();
        }

        public async Task AddCommentAsync()
        {
            await _addCommentView.AddCommentAsync();
        }

        public async Task ListCommentsAsync()
        {
            await _listCommentsView.ListCommentsAsync();
        }
    }
}