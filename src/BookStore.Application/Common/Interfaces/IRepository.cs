using BookStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> QueryContext(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> QueryPage(int? skip = null, int? take = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindAsync(object keyValue);
        Task<TEntity> FindAsync(List<object> keyValues);
        Task<TEntity> SingleAsync(TEntity entity);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void AddRange(List<TEntity> entities);
        void DeleteRange(List<TEntity> entities);
        void UpdateRange(List<TEntity> entities);
        Task<int> SaveAsync();
    }
}
