using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Common;

namespace BookStore.Infrastructure.Data
{
    public class CatalogRepository<TEntity> : Repository<TEntity>, ICatalogRepository<TEntity> where TEntity : BaseEntity
    {
        public CatalogRepository(CatalogContext context) : base(context)
        {
        }

        protected CatalogContext Db { get { return (CatalogContext)DbContext; } }
    }
}
