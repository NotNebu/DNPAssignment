namespace RepositoryContracts
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FileRepository<T> _fileRepository;
        
        public Repository(string fileName)
        {
            _fileRepository = new FileRepository<T>(fileName);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _fileRepository.GetAllAsync();
        }

        public Task<T> AddAsync(T entity)
        {
            return _fileRepository.AddAsync(entity);
        }

        public Task<T> GetByIdAsync(int id)
        {
            return _fileRepository.GetByIdAsync(id);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _fileRepository.UpdateAsync(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _fileRepository.DeleteAsync(id);
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            return _fileRepository.UsernameExistsAsync(username);
        }
    }
}