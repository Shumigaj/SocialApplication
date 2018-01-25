using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialApplication.Business;
using SocialApplication.Business.ExceptionHandling;
using SocialApplication.Core.Contracts;
using SocialApplication.Filters;
using SocialApplication.Storage.Providers;
using Swashbuckle.AspNetCore.Swagger;

namespace SocialApplication
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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AppExceptionFilter));
                options.Filters.Add(typeof(ValidateModelAttribute));
            });

            services.AddSingleton<INewsProvider, NewsProvider>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<ICommentsProvider, CommentsProvider>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllParametersInCamelCase();
                options.DescribeStringEnumsInCamelCase();
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Social application API",
                        Version = "v1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Social application API (v1)");
                });
        }
    }
}
