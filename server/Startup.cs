using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pepper.Commons.Osu;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
using DbContext = vault.Databases.DbContext;

namespace vault
{
    public class Startup
    {
        private static string baseApiPath = "api";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<HttpClient>();

            services.AddDbContext<DbContext>(builder =>
            {
                var connectionString = Environment.GetEnvironmentVariable("MARIADB_CONNECTION_STRING")!;
                builder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.CommandTimeout(3600).SetSqlModeOnOpen())
                    .EnableDetailedErrors();
            });
            services.AddAPIClientStore(credentials =>
            {
                credentials.OAuth2ClientId = int.Parse(Environment.GetEnvironmentVariable("OSU_OAUTH2_CLIENT_ID")!);
                credentials.OAuth2ClientSecret = Environment.GetEnvironmentVariable("OSU_OAUTH2_CLIENT_SECRET")!;
                credentials.LegacyAPIKey = Environment.GetEnvironmentVariable("OSU_API_KEY");
            });
            
            services.AddControllers().AddNewtonsoftJson();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.ToString());
                options.DocumentFilter<SwaggerPrefixDocumentFilter>(baseApiPath);
                options.AddEnumsWithValuesFixFilters();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    o.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.Map($"/{baseApiPath}", appBuilder =>
            {
                appBuilder.UseRouting();
                appBuilder.UseCors();
                appBuilder.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}");
                });
            });
        }
    }
}