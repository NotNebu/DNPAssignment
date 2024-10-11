namespace RepositoryContracts;

// Interface til at tilgå filer - Bliver ikke rigtig brugt tho
public interface IFileRepository<T> : IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    
}