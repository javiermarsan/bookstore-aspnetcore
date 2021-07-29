using BookStore.Application.Common.Interfaces;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<CatalogContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddScoped(typeof(ICatalogRepository<>), typeof(CatalogRepository<>));
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
