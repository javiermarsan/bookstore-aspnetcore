using BookStore.Application.Common.Interfaces;
using BookStore.Infrastructure.Data;
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
            services.AddDbContext<EfContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IEfContext>(provider => provider.GetService<EfContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
