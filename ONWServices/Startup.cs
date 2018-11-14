using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONWServices.Repositories;
using ONWServices.Settings;

namespace ONWServices
{
    public class Startup
    {
        private const string MONGODB_CONNECTION_STRING_SECTION_NAME = "MongoDb:ConnectionString";
        private const string MONGODB_DATABASE_NAME_SECTION_NAME = "MongoDb:Database";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<MongoDbSettings>(
                settings =>
                {
                    settings.ConnectionString = Configuration.GetSection(MONGODB_CONNECTION_STRING_SECTION_NAME).Value;
                    settings.Database = Configuration.GetSection(MONGODB_DATABASE_NAME_SECTION_NAME).Value;
                }
            );
            
            services.AddSingleton<IOnwDbContext, OnwDbContext>();
            services.AddSingleton<IGameRepository, GameRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
