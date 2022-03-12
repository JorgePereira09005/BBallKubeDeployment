using BBallService.Data;
using BBallService.Repository;
using BBallService.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BBallService
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
            //server=localhost;user=hbstudent;password=hbstudent;database=BBallService;
            //http://localhost:8080/api/picture
            string mySqlConnectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<BBallContext>(options => options
                .UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            services.AddScoped<PlayerRepository, PlayerRepositoryImpl>();

            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions
                .ReferenceHandler = ReferenceHandler.Preserve);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers();

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BBallService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
                options.WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BBallService v1"));
            }

            /*app.UseHttpsRedirection();*/

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
