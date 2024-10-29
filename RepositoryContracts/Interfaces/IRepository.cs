// Generic Repository to reduce code duplication

namespace RepositoryContracts
{
    /// <summary>
    /// Generic repository interface that provides basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Asynchronously gets all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Asynchronously adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Asynchronously gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Asynchronously deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Asynchronously checks if a username exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
        Task<bool> UsernameExistsAsync(string username);
    }
}