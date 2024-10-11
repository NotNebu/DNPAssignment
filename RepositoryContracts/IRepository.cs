// Generic Repository to reduce code duplication
/* It began with the forging of the great interfaces.
Three were given to the entities, immortal, wisest and fairest of all beings.
Seven to the repositories, great miners and craftsmen of the mountain halls.
And nine, nine interfaces were gifted to the controllers, who above all else desire power.
For within these interfaces was bound the strength and the will to govern each domain.
But they were all of them deceived, for another interface was made.
In the land of Mordor, in the fires of Mount Doom, the dark lord Sauron
forged in secret a master interface, to control all others.
And into this interface he poured his cruelty, his malice and his will to dominate all life.
One interface to rule them all. */

namespace RepositoryContracts;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> UsernameExistsAsync(string username);
}