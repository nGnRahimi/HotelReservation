using Domain.BaseEntity;
using Infrastructure.UnitOfWork;
using System.Linq.Expressions;

namespace Domain.BaseRepository
{
    public interface IBaseRepository<T , K> where T : IBaseEntity<K> where K : IEquatable<K>
    {
        Task<T> FindAsync(K id);
        IQueryable<T> Get (Expression<Func<T , bool>>? predicate);

        IUnitOfWork UnitOfWork { get; }


        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);


        void AddRange(List<T> entities);

        void UpdateRange(List<T> entities);

        void DeleteRange(List<T> entities);

    }
}
