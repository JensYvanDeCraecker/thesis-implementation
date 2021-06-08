using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using ParcelTracker.Options;

namespace ParcelTracker
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AppOptions appOptions = configuration.GetSection("App").Get<AppOptions>();
            if (appOptions.UseCache)
                services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Cache"));
            services.AddDbContext<ParcelDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Database")));
            services.Configure<AppOptions>(configuration.GetSection("App"));
            services.AddTransient(sp => sp.GetRequiredService<IOptions<AppOptions>>().Value);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            if (env.IsDevelopment())
                services.AddCors(options => options.AddDefaultPolicy(builder => builder.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod()));
            if (env.IsProduction())
                services.AddHostedService<MigrateHostedService>();
            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory logger)
        {
            AppOptions appOptions = app.ApplicationServices.GetRequiredService<AppOptions>();
            logger.CreateLogger(typeof(Startup)).LogInformation("Use cache: {0}", appOptions.UseCache);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors();
            }
            app.UseResponseCaching();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    if (ctx.File.PhysicalPath.Contains("_next/static"))
                    {
                        ctx.Context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                        {
                            MaxAge = TimeSpan.FromDays(365),
                            Public = true
                        };
                    }
                    else
                    {
                        ctx.Context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                        {
                            NoCache = true,
                            Public = true
                        };
                    }
                }
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}