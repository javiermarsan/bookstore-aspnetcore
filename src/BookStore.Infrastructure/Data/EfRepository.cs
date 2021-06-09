using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.ApplicationCore.Entities;
using BookStore.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookStore.Infrastructure.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected EfContext DbContext { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }

        public EfRepository(EfContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> QueryContext(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Query(null, null, null, null, false, includeProperties);
        }

        public virtual IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Query(null, null, null, null, true, includeProperties);
        }

        public virtual IQueryable<TEntity> QueryPage(int? skip = null, int? take = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Query(null, null, skip, take, true, includeProperties);
        }

        public virtual async Task<TEntity> FindAsync(object keyValue)
        {
            var keyValues = new List<object>() { keyValue };
            return await FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(List<object> keyValues)
        {
            try
            {
                var entityDB = await DbSet.FindAsync(keyValues.ToArray());
                return entityDB;
            }
            catch (Exception)
            {
                throw new Exception("Entity does not exists.");
            }
        }

        public virtual async Task<TEntity> SingleAsync(TEntity entity)
        {
            try
            {
                var type = entity.GetType();
                var properties = DbContext.Model.FindEntityType(typeof(TEntity)).FindPrimaryKey().Properties;
                var keyValues = new List<object>();

                foreach (var member in properties)
                {
                    var info = type.GetProperty(member.Name);
                    var tempValue = info.GetValue(entity, null);

                    keyValues.Add(tempValue);
                }

                var entityDB = await DbSet.FindAsync(keyValues.ToArray());
                return entityDB;
            }
            catch (Exception) { }

            return null;
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void AddRange(List<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void DeleteRange(List<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void UpdateRange(List<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        // Query(m => m.id == entityId, t => t.OrderBy(m => m.date), null, null, true, m => m.Address);
        private IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            bool asNoTracking = true,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = asNoTracking ? DbSet.AsNoTracking() : DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (Expression<Func<TEntity, object>> include in includeProperties)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
