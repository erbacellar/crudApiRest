﻿using CrudApiRest.Application.Interfaces;
using CrudApiRest.Application.Services;
using CrudApiRest.Data.Interfaces;
using CrudApiRest.Data.Repository;
using CrudApiRest.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Globalization;
using System.Collections.Generic;

namespace CrudApiRest.WebApi
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
            services.AddMvc();
            ContextDbConfig(services);
            IocServices(services);
            SetupSwagger(services);
            SetupCorsPolicy(services);
        }

        private static void IocServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddSingleton<IMockData, MockData>();
        }

        private static void SetupSwagger(IServiceCollection services)
        {
            services.ConfigureSwaggerGen(x =>
            {
                x.IncludeXmlComments("CrudApiRest.WebApi.xml");
            });

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.CustomSchemaIds(x => x.FullName);
                options.SwaggerDoc("v1", new Info
                {
                    Title = "CrudApiRest",
                    Version = "v1",
                    Description = "Crud Users",
                    TermsOfService = "Termos do serviço",
                    Contact = new Contact()
                    {
                        Name = "Ernane Ribas"
                    }
                });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                options.AddSecurityDefinition(
                    "Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Insert 'Bearer' + 'token' into field",
                        Name = "Authorization",
                        Type = "apiKey"
                    });

                options.AddSecurityRequirement(security);

            });
        }

        private static void SetupCorsPolicy(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                                            builder => builder.AllowAnyHeader()
                                            .AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowCredentials()
                        ));
        }

        private void ContextDbConfig(IServiceCollection services)
        {
            services.AddDbContext<DbUsersContext>(options => options.UseSqlServer(Configuration.GetConnectionString("UsersDb")));
            services.AddScoped<IDbUsersContext>(provider => provider.GetService<DbUsersContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint("v1/swagger.json", "API V1");
              });

            // Adicionando CORS
            app.UseCors("CorsPolicy");

            app.UseMvcWithDefaultRoute();
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

        }
    }
}
