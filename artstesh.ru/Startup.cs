using System;
using artstesh.data.DbContext;
using artstesh.data.Repositories;
using artstesh.ru.Services;
using C2c.Config;
using C2c.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace artstesh.ru
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
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/logout";
                    options.Cookie.Name = ".AspNetCore.Cookies";
                    options.Cookie.Expiration = TimeSpan.FromHours(5);
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DataContext"));
            });
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IConfigSettings, ConfigSettings>();
            services.AddScoped<ICacheHelper, CacheHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IDistributedCache distributedCache)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Reading}/{action=Index}/{id?}");
            });
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(Convert.ToDouble(Configuration.GetSection("appkeys")["CacheLifeTime"])));
            distributedCache.Set("News", new byte[0], cacheEntryOptions);
        }
    }
}