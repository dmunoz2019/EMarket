using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    // <T> se usa para que sea generico y se pueda usar en cualquier clase
    public interface IGenericRepository<T> where T :  Base
    {
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();

    }
}
