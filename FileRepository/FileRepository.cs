using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository;

public class FileRepository<T> : IRepository<T> where T : class
{
    private readonly string filePath;

    // Constructor til at initialisere filstien
    public FileRepository(string fileName)
    {
        filePath = $"{fileName}.json";
        
        // Opretter filen, hvis den ikke findes
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    // Tilføjer en ny entitet til filen asynkront
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

    // Henter alle entiteter fra filen asynkront
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await GetAllEntitiesAsync();
    }

    // Henter en entitet ud fra dens ID asynkront
    public async Task<T> GetByIdAsync(int id)
    {
        var entities = await GetAllEntitiesAsync();
        return entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id)
               ?? throw new InvalidOperationException();
    }

    // Opdaterer en eksisterende entitet asynkront
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

    // Sletter en entitet ud fra dens ID asynkront
    public async Task<bool> DeleteAsync(int id)
    {
        var entities = await GetAllEntitiesAsync();
        var entity = entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id);

        if (entity == null) return false;

        entities.Remove(entity);
        await SaveAllEntitiesAsync(entities);
        return true;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        if (typeof(T) == typeof(User))
        {
            var users = await GetAllEntitiesAsync();
            return users.Any(e => ((User)(object)e).UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        throw new InvalidOperationException("UsernameExistsAsync is only supported for User entities.");
    }

    // Metode til at hente alle entiteter fra filen
    private async Task<List<T>> GetAllEntitiesAsync()
    {
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }

    // Metode til at gemme alle entiteter i filen
    private async Task SaveAllEntitiesAsync(List<T> entities)
    {
        string json = JsonSerializer.Serialize(entities);
        await File.WriteAllTextAsync(filePath, json);
    }
}