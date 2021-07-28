using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infrastructure.Data.Configurations
{
    public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreationDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(e => e.ProductId).IsRequired();

            builder.Property(e => e.BasketId).IsRequired();
        }
    }
}
