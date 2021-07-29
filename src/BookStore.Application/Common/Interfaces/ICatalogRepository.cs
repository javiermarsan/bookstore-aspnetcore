using BookStore.Domain.Common;

namespace BookStore.Application.Common.Interfaces
{
    public interface ICatalogRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
    }
}
