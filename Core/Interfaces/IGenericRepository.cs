using Core.Entities;
using Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    // <T> se usa para que sea generico y se pueda usar en cualquier clase
    public interface IGenericRepository<T> where T : Base
    {
        // Read operations
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec);

        // Create operation
        Task<T> AddAsync(T entity);

        // Update operation
        Task UpdateAsync(T entity);

        // Delete operation
        Task DeleteAsync(T entity);

        Task<int> CountAsync(ISpecification<T> spec);
    }
}
