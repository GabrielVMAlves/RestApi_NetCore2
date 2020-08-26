using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestApi_NetCore2.Hypermedia;
using RestApi_NetCore2.Models.Context;
using RestApi_NetCore2.Repository;
using RestApi_NetCore2.Repository.Generic;
using RestApi_NetCore2.Repository.Implementation;
using RestApi_NetCore2.Services;
using RestApi_NetCore2.Services.Implementations;
using System;
using System.Collections.Generic;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;

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


            HyperMediaFilterOptions filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());

            //services.AddApiVersioning();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new Info {
                        Title = "Restfull API - .NET CORE 2.0",
                        Version = "v1"
                    });
            });

            #region Services
            services.AddSingleton(filterOptions);
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IBookService, BookService>();
            #endregion

            #region Repositorys
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
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

            app.UseSwagger();
            app.UseSwaggerUI( c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
            });

            RewriteOptions option = new RewriteOptions();

            option.AddRedirect("^$", "swagger");

            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "DefaultApi",
                    template: "{controller=Values}/{id?}"
                    );
            });
        }
    }
}
