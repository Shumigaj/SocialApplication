using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialApplication.Business;
using SocialApplication.Core.Contracts;
using SocialApplication.Filters;
using SocialApplication.Storage.Providers;

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
                options.Filters.Add(new ValidateModelFilterAttribute());
            });

            services.AddSingleton<INewsProvider, NewsProvider>();
            services.AddTransient<INewsRepository, NewsRepository>();
            services.AddTransient<ICommentsProvider, CommentsProvider>();
            services.AddTransient<ICommentsRepository, CommentsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
