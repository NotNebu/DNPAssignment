using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository;

/// <summary>
/// File-based implementation of the generic repository.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public class FileRepository<T> : IRepository<T> where T : class
{
    private readonly string filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileRepository{T}"/> class with the specified file name.
    /// </summary>
    /// <param name="fileName">The name of the file to store the entities.</param>
    public FileRepository(string fileName)
    {
        filePath = $"{fileName}.json";
        
        // Creates the file if it does not exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    /// <summary>
    /// Asynchronously adds a new entity to the file.
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
    /// Asynchronously gets all entities from the file.
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
    /// Asynchronously checks if a username exists. Only supported for User entities.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns>A task that represents the asynchronous operation. The task result indicates whether the username exists.</returns>
    public async Task<bool> UsernameExistsAsync(string username)
    {
        if (typeof(T) == typeof(User))
        {
            var users = await GetAllEntitiesAsync();
            return users.Any(e => ((User)(object)e).UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        throw new InvalidOperationException("UsernameExistsAsync is only supported for User entities.");
    }

    /// <summary>
    /// Gets all entities from the file.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities.</returns>
    private async Task<List<T>> GetAllEntitiesAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    /// <summary>
    /// Saves all entities to the file.
    /// </summary>
    /// <param name="entities">The list of entities to save.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task SaveAllEntitiesAsync(List<T> entities)
    {
        string json = JsonSerializer.Serialize(entities);
        await File.WriteAllTextAsync(filePath, json);
    }
}