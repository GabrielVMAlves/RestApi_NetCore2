using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestApi_NetCore2.Models.Context;
using RestApi_NetCore2.Repository;
using RestApi_NetCore2.Repository.Implementation;
using RestApi_NetCore2.Services.Implementations;

namespace RestApi_NetCore2
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
            var connection = Configuration["MySqlConnections:MySqlConnectionString"];

            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region Services
            services.AddScoped<IPersonService, PersonService>();
            #endregion

            #region Repositorys
            services.AddScoped<IPersonRepository, PersonRepository>();
            #endregion

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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
