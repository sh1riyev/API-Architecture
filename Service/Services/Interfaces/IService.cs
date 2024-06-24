using System;
using System.Linq.Expressions;
using Domain.Common;

namespace Service.Services.Interfaces
{
	public interface IService<T> where T:BaseEntity
	{
        Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes);
        Task Create(T entity);
        Task Delete(int id);
        Task Update(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> predicate = null);
    }
}

