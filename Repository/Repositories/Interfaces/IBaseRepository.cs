using System;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
	public interface IBaseRepository<T>
	{
        Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task Create(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);
    }
}

