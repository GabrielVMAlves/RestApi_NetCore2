using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestApi_NetCore2.Models.Context;
using RestApi_NetCore2.Repository;
using RestApi_NetCore2.Repository.Implementation;
using RestApi_NetCore2.Services.Implementations;
using System;
using System.Collections.Generic;

namespace RestApi_NetCore2
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _env { get; }
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _env = env;
            _logger = logger;
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnections:MySqlConnectionString"];

            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if(_env.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "DB/Migrations", "DB/DataSet" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }catch(Exception e)
                {
                    _logger.LogCritical("Database migration failed!", e);
                    throw e;
                }
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddApiVersioning();

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
