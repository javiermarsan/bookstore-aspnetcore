using AutoMapper;
using BookStore.Api.Features.Authors.Commands;
using BookStore.Api.Features.Authors.Queries;
using BookStore.ApplicationCore.Interfaces;
using BookStore.ApplicationCore.Services;
using BookStore.Infrastructure.Data;
using BookStore.Infrastructure.Identity.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IBasketService, BasketService>();

            services.AddControllers().AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<CreateAuthorCommand.CreateCommandValidator>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore.Api", Version = "v1" });
            });

            services.AddDbContext<EfContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DbConnection"));
            });

            services.AddMediatR(typeof(CreateAuthorCommand.CreateCommandHandler).Assembly);
            services.AddAutoMapper(typeof(GetAuthorListQuery));

            // Token
            services.AddTransient<ITokenService, TokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "bearer";
            })
            .AddJwtBearer("bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenService.ServerSigningPassword)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication(); // Token
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
