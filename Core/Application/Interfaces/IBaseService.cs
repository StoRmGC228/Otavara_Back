namespace Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBaseService<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(int id);
}