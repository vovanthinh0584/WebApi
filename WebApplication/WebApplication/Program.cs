using System;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            // use this to allow command line parameters in the config
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("log.json")
                .AddCommandLine(args)
                .Build();

            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            var hostUrl = configuration["Urls"];
            if (string.IsNullOrEmpty(hostUrl))
                hostUrl = "http://0.0.0.0:5000";

            bool console = args.Any(arg => arg.ToLower() == "--console");
            bool service = args.Any(arg => arg.ToLower() == "--service");

            try
            {
                IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(configuration))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (service)
                    {
                        webBuilder.UseKestrel();
                        webBuilder.UseUrls(hostUrl);
                    }
                    else if (console)
                    {
                        webBuilder.UseKestrel();
                        webBuilder.UseUrls(hostUrl);
                    }
                    else // running in iis
                    {
                        //**Note:**When using ASP.NET Core 2.2 and you want to use In-Process hosting, replace * *.UseIISIntegration() * *with * *.UseIIS() * *, otherwise you'll get startup errors.
                        webBuilder.UseIISIntegration();
                    }

                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config
                            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                            .AddConfiguration(configuration)
                            .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                            .AddEnvironmentVariables();
                    });

                    if (service)
                    {
                        webBuilder.UseUrls(hostUrl);
                    }
                    else if (console)
                    {
                        webBuilder.UseUrls(hostUrl);
                    }
                    else // running in iis
                    {
                        //**Note:**When using ASP.NET Core 2.2 and you want to use In-Process hosting, replace * *.UseIISIntegration() * *with * *.UseIIS() * *, otherwise you'll get startup errors.
                        //webBuilder.UseIISIntegration();
                    }

                    webBuilder.UseStartup<Startup>();
                });

                if (service)
                {
                    hostBuilder.UseWindowsService();
                }

                return hostBuilder;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
