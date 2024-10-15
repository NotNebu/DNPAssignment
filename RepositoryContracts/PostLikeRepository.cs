using System.Text.Json;

namespace RepositoryContracts
{
    public class FileRepository<T> : IRepository<T> where T : class
    {
        private readonly string filePath;
        
        public FileRepository(string fileName)
        {
            filePath = $"{fileName}.json";
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetAllEntitiesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entities = await GetAllEntitiesAsync();
            return entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id)
                   ?? throw new InvalidOperationException();
        }

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

        public async Task<bool> DeleteAsync(int id)
        {
            var entities = await GetAllEntitiesAsync();
            var entity = entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id);

            if (entity == null) return false;

            entities.Remove(entity);
            await SaveAllEntitiesAsync(entities);
            return true;
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            throw new NotSupportedException("UsernameExistsAsync is not supported for this entity.");
        }

        private async Task<List<T>> GetAllEntitiesAsync()
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        private async Task SaveAllEntitiesAsync(List<T> entities)
        {
            string json = JsonSerializer.Serialize(entities);
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
