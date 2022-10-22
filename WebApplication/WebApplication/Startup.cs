using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Utils;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        ILogger _logger;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            IAppSettings appSettings = appSettingsSection.Get<AppSettings>();
            var rootFolder = Directory.GetCurrentDirectory();
            string[] stringQueries = Directory.GetFiles(rootFolder + @"\" + appSettings.Queries_Folder, " *.xml", SearchOption.AllDirectories);
            services.AddControllers();
            // configure strongly typed settings objects
         
             var key = System.Text.Encoding.ASCII.GetBytes(appSettings.Secret);
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger("IPDMobileService");
            services.AddSingleton<IDao>(new Dao(appSettings.Database, stringQueries));
            var messagePath = rootFolder + @"\" + appSettings.Message_Path;
            services.AddSingleton<IMessage>(new Message(messagePath));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })                  
         .AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ClockSkew = TimeSpan.Zero
             };
         });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
