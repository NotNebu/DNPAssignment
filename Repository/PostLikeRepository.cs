using Entities;
using RepositoryContracts;

namespace Repository;

public class PostLikeRepository : IRepository<PostLike>
{
    private readonly FileRepository<PostLike> _fileRepository;

    // Constructor to initialize the file repository
    public PostLikeRepository(string fileName)
    {
        _fileRepository = new FileRepository<PostLike>(fileName);
    }
    
    public Task<PostLike> AddAsync(PostLike entity)
    {
        return _fileRepository.AddAsync(entity);
    }
    
    public Task<bool> DeleteAsync(int id)
    {
        return _fileRepository.DeleteAsync(id);
    }
    
    public Task<IEnumerable<PostLike>> GetAllAsync()
    {
        return _fileRepository.GetAllAsync();
    }
    
    public Task<PostLike> GetByIdAsync(int id)
    {
        return _fileRepository.GetByIdAsync(id);
    }
    
    public Task<PostLike> UpdateAsync(PostLike entity)
    {
        return _fileRepository.UpdateAsync(entity);
    }
    
    public Task<bool> UsernameExistsAsync(string username)
    {
        throw new NotImplementedException();
    }
}