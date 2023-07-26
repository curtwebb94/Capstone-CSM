using CSM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSM
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
            // Register IUserProfileRepository and its implementation UserProfileRepository
            services.AddTransient<IUserProfileRepository, UserProfileRepository>();

            // Register ICodeSnippetRepository and its implementation CodeSnippetRepository
            services.AddTransient<ICodeSnippetRepository, CodeSnippetRepository>();

            // Register ITagRepository and its implementation TagRepository
            services.AddTransient<ITagRepository, TagRepository>();

            // Register ICodeSnippetTagRepository and its implementation CodeSnippetTagRepository
            services.AddTransient<ICodeSnippetTagRepository, CodeSnippetTagRepository>();

            // Register IFavoriteSnippetRepository and its implementation FavoriteSnippetRepository
            services.AddTransient<IFavoriteSnippetRepository, FavoriteSnippetRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CSM", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSM v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
