using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Infrastructure.Data.Interfaces;
using Cashback.Infrastructure.Data.Models;
using Cashback.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Cashback.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<CashbackContext>(options =>
            {
                //options.UseInMemoryDatabase("cashback");
                options.UseSqlServer("Server=localhost;Database=CashbackTests;Trusted_Connection=True;");
            });

            //Register spotify service
            SpotifyService.IoC.RegisterServices(services, Configuration);

            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICatalogRepository, CatalogRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IBasketRepository, BaskeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Cashback API",
                    Description = "Backend engineer test - BeBlue",
                    TermsOfService = "None"
                });

                var basePath = AppContext.BaseDirectory;
                //var xmlPath = System.IO.Path.Combine(basePath, "GeekBurger.StoreCatalog.xml");
                //c.IncludeXmlComments(xmlPath);
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cashback API v1");
            });
        }
    }
}
