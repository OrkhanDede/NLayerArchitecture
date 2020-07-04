using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLayerArchitecture.Application.Interfaces;
using NLayerArchitecture.Application.Services;
using NLayerArchitecture.Core.Logger;
using NLayerArchitecture.Core.Repositories;
using NLayerArchitecture.Infrastructure.Data;
using NLayerArchitecture.Infrastructure.Logging;
using NLayerArchitecture.Infrastructure.Repository;
using NLayerArchitecture.Infrastructure.Repository.Base;

namespace NLayerArchitecture.API
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
            //dependencies
            ConfigureNLayerServices(services);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureNLayerServices(IServiceCollection services)
        {
            // Add Infrastructure Layer
            ConfigureDatabase(services);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            //Add Application Layer
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
        public void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<NLayerArchitectureDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
