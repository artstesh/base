﻿using System;
using artstesh.core.Config;
using artstesh.core.Helpers;
using artstesh.core.RemoteAgents;
using artstesh.data.DbContext;
using artstesh.data.Repositories;
using artstesh.data.Services;
using artstesh.ru.Helpers;
using artstesh.ru.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                    options.Cookie.Expiration = TimeSpan.FromHours(155);
                });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMemoryCache();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DataContext"));
            });
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IArticleCacheService, ArticleCacheService>();
            services.AddSingleton<IConfigSettings, ConfigSettings>();
            services.AddScoped<ICacheHelper, CacheHelper>();
            services.AddSingleton<IGoogleRecaptchaService, GoogleRecaptchaService>();
            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddSingleton<IMailAgent, MailAgent>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IMessageHelper, MessageHelper>();
            services.AddScoped<IAlarmService, AlarmService>();
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
                    "default",
                    "{controller=Reading}/{action=Index}/{id?}");
            });
            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(Convert.ToDouble(Configuration.GetSection("appkeys")["CacheLifeTime"])));
            distributedCache.Set("News", new byte[0], cacheEntryOptions);
        }
    }
}