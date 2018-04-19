using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSNetwork18.DAL;
using MSNetwork18.DAL.Interface;
using MSNetwork18.WebAPI.Middleware;

namespace MSNetwork18.WebAPI
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
            services.AddTransient<ISQLDocumentRepository, SQLDocumentRepository>();
            services.AddTransient<ISQLStoredProcedureRepository, SQLStoredProcedureRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<ElapsedTimeMiddleware>();
            }

            app.UseMvc();
        }
    }
}
