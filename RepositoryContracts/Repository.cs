namespace RepositoryContracts
{
    /// <summary>
    /// Generic repository class that provides basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FileRepository<T> _fileRepository;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file to be used by the repository.</param>
        public Repository(string fileName)
        {
            _fileRepository = new FileRepository<T>(fileName);
        }

        /// <summary>
        /// Asynchronously gets all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of entities.</returns>
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _fileRepository.GetAllAsync();
        }

        /// <summary>
        /// Asynchronously adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        public Task<T> AddAsync(T entity)
        {
            return _fileRepository.AddAsync(entity);
        }

        /// <summary>
        /// Asynchronously gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        public Task<T> GetByIdAsync(int id)
        {
            return _fileRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        public Task<T> UpdateAsync(T entity)
        {
            return _fileRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Asynchronously deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public Task<bool> DeleteAsync(int id)
        {
            return _fileRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Asynchronously checks if a username exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
        public Task<bool> UsernameExistsAsync(string username)
        {
            return _fileRepository.UsernameExistsAsync(username);
        }
    }
}