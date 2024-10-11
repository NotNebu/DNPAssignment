using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository
{
    public class FileRepository<T> : IFileRepository<T> where T : class
    {
        private readonly string filePath;

        // Constructor til at initialisere filstien og oprette filen, hvis den ikke eksisterer
        public FileRepository(string fileName)
        {
            // Sætter filsti baseret på det givne filnavn
            filePath = $"{fileName}.json";
            
            // Opretter filen, hvis den ikke findes
            if (!File.Exists(filePath))
            {
                // Opretter en tom JSON-liste
                File.WriteAllText(filePath, "[]");
            }
        }

        // Tilføjer en ny entity til filen asynkront
        public async Task<T> AddAsync(T entity)
        {
            // Henter eksisterende entities
            var entities = await GetAllEntitiesAsync();
            
            // Finder Id-property på typen T
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty != null)
            {
                // Beregner nyt ID til den nye entity
                int maxId = entities.Any() ? entities.Max(e => (int)idProperty.GetValue(e)) : 0;
                
                // Sætter det nye ID på den nye entity
                idProperty.SetValue(entity, maxId + 1);
            }
            
            // Tilføjer den nye entity til listen af entities
            entities.Add(entity);
            
            // Gemmer de opdaterede entities til filen
            await SaveAllEntitiesAsync(entities);
            
            // Returnerer den nye entity
            return entity;
        }

        // Henter alle entities asynkront
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Returnerer alle entities
            return await GetAllEntitiesAsync();
        }

        // Henter en entity ud fra dens Id asynkront
        public async Task<T> GetByIdAsync(int id)
        {
            // Henter alle entities
            var entities = await GetAllEntitiesAsync();
            
            // Finder og returnerer entity med det givne Id
            return entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id)
                   
                   // Kaster en InvalidOperationException, hvis entity ikke findes
                   ?? throw new InvalidOperationException();
        }

        // Opdaterer en entity asynkront
        public async Task<T> UpdateAsync(T entity)
        {
            // Henter alle entities
            var entities = await GetAllEntitiesAsync();
            
            // Finder Id-property på typen T
            var idProperty = typeof(T).GetProperty("Id");
            
            // Kaster en InvalidOperationException, hvis Id-property ikke findes
            if (idProperty == null) throw new InvalidOperationException();

            // Finder Id på den givne entity
            var entityId = (int)idProperty.GetValue(entity);
            
            // Finder index for den givne entity
            var entityIndex = entities.FindIndex(e => (int)idProperty.GetValue(e) == entityId);
            
            // Kaster en InvalidOperationException, hvis entity ikke findes
            if (entityIndex == -1) throw new InvalidOperationException();

            // Opdaterer entity i listen af entities
            entities[entityIndex] = entity;
            
            // Gemmer de opdaterede entities til filen
            await SaveAllEntitiesAsync(entities);
            
            // Returnerer den opdaterede entity
            return entity;
        }

        // Sletter en entity ud fra dens Id asynkront
        public async Task<bool> DeleteAsync(int id)
        {
            
            //  Henter alle entities
            var entities = await GetAllEntitiesAsync();
            
            // Finder og fjerner entity med det givne Id
            var entity = entities.SingleOrDefault(e => (int)typeof(T).GetProperty("Id")?.GetValue(e) == id);
            
            // Returnerer false, hvis entity ikke findes
            if (entity == null) return false;

            // Fjerner entity fra listen af entities
            entities.Remove(entity);
            
            // Gemmer de opdaterede entities til filen
            await SaveAllEntitiesAsync(entities);
            
            // Returnerer true, hvis entity blev slettet
            return true;
        }
        
        // Henter en entity ud fra dens Id asynkront
        public async Task<bool> UsernameExistsAsync(string username)
        {
            // Kaster en InvalidOperationException, hvis T ikke er en User
            if (typeof(T) == typeof(User))
            {
                // Henter alle entities
                var users = await GetAllEntitiesAsync();
                
                // Returnerer true, hvis der findes en User med det givne brugernavn
                return users.Any(e => ((User)(object)e).UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            }

            // Kaster en InvalidOperationException, hvis T ikke er en User
            throw new InvalidOperationException("UsernameExistsAsync is only supported for User entities.");
        }

        // Henter en entity ud fra dens Id asynkront
        private async Task<List<T>> GetAllEntitiesAsync()
        {
            // Læser JSON fra filen
            string json = await File.ReadAllTextAsync(filePath);
            
            // Returnerer deserialiseret liste af entities
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        // Gemmer alle entities asynkront
        private async Task SaveAllEntitiesAsync(List<T> entities)
        {
            // Serialiserer liste af entities til JSON
            string json = JsonSerializer.Serialize(entities);
            
            // Overskriver filen med den serialiserede JSON
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
