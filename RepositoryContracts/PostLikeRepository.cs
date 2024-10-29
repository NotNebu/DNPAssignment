using System.Text.Json;

namespace RepositoryContracts
{
    /// <summary>
    /// Generic file repository class that provides basic CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class FileRepository<T> : IRepository<T> where T : class
    {
        private readonly string filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository{T}"/> class.
        /// </summary>
        /// <param name="fileName">The name of the file to be used by the repository.</param>
        public FileRepository(string fileName)
        {
            filePath = $"{fileName}.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        /// <summary>
        /// Asynchronously adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        public async Task<T> AddAsync(T entity)
        {
            var entities = await GetAllEntitiesAsync();
            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty != null)
            {
                int maxId = entities.Any() ? entities.Max(e => (int)idProperty.GetValue(e)) : 0;
                idProperty.SetValue(entity, maxId + 1);
            }

            entities.Add(entity);
            await SaveAllEntitiesAsync(entities);
            return entity;
        }

        /// <summary>
        /// Asynchronously gets all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable of entities.</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllEntitiesAsync();
        }

        /// <summary>
        /// Asynchronously gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var entities = await GetAllEntitiesAsync();
            return entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id)
                   ?? throw new InvalidOperationException();
        }

        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        public async Task<T> UpdateAsync(T entity)
        {
            var entities = await GetAllEntitiesAsync();
            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null) throw new InvalidOperationException();

            var entityId = (int)idProperty.GetValue(entity);
            var entityIndex = entities.FindIndex(e => (int)idProperty.GetValue(e) == entityId);

            if (entityIndex == -1) throw new InvalidOperationException();

            entities[entityIndex] = entity;
            await SaveAllEntitiesAsync(entities);
            return entity;
        }

        /// <summary>
        /// Asynchronously deletes an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the deletion was successful.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var entities = await GetAllEntitiesAsync();
            var entity = entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id);

            if (entity == null) return false;

            entities.Remove(entity);
            await SaveAllEntitiesAsync(entities);
            return true;
        }

        /// <summary>
        /// Throws a NotSupportedException as UsernameExistsAsync is not supported for this entity.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotSupportedException("UsernameExistsAsync is not supported for this entity.");
        }

        /// <summary>
        /// Asynchronously gets all entities from the file.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
        private async Task<List<T>> GetAllEntitiesAsync()
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        /// <summary>
        /// Asynchronously saves all entities to the file.
        /// </summary>
        /// <param name="entities">The list of entities to save.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task SaveAllEntitiesAsync(List<T> entities)
        {
            string json = JsonSerializer.Serialize(entities);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}