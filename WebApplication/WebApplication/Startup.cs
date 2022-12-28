using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Services;
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
            services.AddCors();
            var key = Encoding.ASCII.GetBytes(new AppSettings(Configuration).Secret);
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger("IMobileService");
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers(o => o.RespectBrowserAcceptHeader = true)
                .AddJsonOptions(options =>
                {
                    // Use the default property (Pascal) casing.
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;// JsonNamingPolicy.CamelCase;
                })
                .AddNewtonsoftJson(options =>
                {
                    // Use the default property (Pascal) casing
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true; // Default is true, make it false
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession();
            services.AddMvc().AddSessionStateTempDataProvider();
            // Enable response compression
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Fastest;

            }).AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            //needed for NLog.Web
            //call this in case you need aspnet-user-authtype/aspnet-user-identity
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();

   
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddSingleton<IDao, Dao>();
            services.AddSingleton<IMessage, Message>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IInputRequestService, InputRequestService>();
            services.AddSingleton<IInputDeviceParameterService, InputDeviceParameterService>();
            services.AddSingleton<IGetTaskService, GetTaskService>();
            services.AddSingleton<ISheet033BoilerService,Sheet033BoilerService>();
        }

      public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env, ILoggerFactory logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCookiePolicy();

            //  app.UseHttpsRedirection();

            app.ConfigureExceptionHandler(_logger);

            // Enable compression
            app.UseResponseCompression();
            app.UseCors(builder => builder.SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            //app.UseCors(PolicyOrigins);

            app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
            app.UseStaticFiles(); // For the wwwroot folder

            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".apk"] = "application/vnd.android.package-archive";
            provider.Mappings[".ipa"] = "application/octet-stream";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider
            });
            app.UseMvc();
        }
    }
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Server error: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
