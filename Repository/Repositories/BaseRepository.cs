using System;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();

        }

        public async Task Delete(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;

                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.FirstOrDefaultAsync() : await query.FirstOrDefaultAsync(predicate);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Update(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetAllIncludes(params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return query;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate = null)
        {

            try
            {
                return predicate == null ? false : await _table.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}