using System.Reflection;
using AutoMapper;
using BookStore.Application.Common.Interfaces;
using BookStore.Application.Baskets.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IBasketService, BasketService>();

            return services;
        }
    }
}
