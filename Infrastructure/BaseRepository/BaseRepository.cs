using Domain.BaseEntity;
using Domain.BaseRepository;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BaseRepository
{
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : BaseEntity<K> where K :
        IEquatable<K>
    {
        protected readonly ApplicationDbContext _context;

        protected readonly DbSet<T> _setContext;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _setContext = context.Set<T>();


        }

        public IUnitOfWork UnitOfWork
        {

            get
            {
                return _context;

            }

        }

        public T Add(T entity)
        {
            return _setContext.Add(entity).Entity;
        }

        public void AddRange(List<T> entities)
        {
            _setContext.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _setContext.Remove(entity);
        }

        public void DeleteRange(List<T> entities)
        {
            _setContext.RemoveRange(entities);
        }

        public Task<T> FindAsync(K id)
        {
            var data = _setContext.Where(a => a.Id.Equals(id));
            return data.FirstOrDefaultAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>>? predicate)
        {

            var data = _setContext.AsQueryable();
            if (predicate != null)

            {

                data = data.Where(predicate);

            }

            return data;

        }

        public T Update(T entity)
        {
            return _setContext.Update(entity).Entity;
        }

        public void UpdateRange(List<T> entities)
        {
            _setContext.UpdateRange(entities);
        }
    }
}
