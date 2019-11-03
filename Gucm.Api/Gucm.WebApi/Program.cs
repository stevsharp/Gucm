using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Gucm.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildHost(args).RunAsync();
        }

        public static IWebHost BuildHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureAppConfiguration((context, config) => { })
                   .ConfigureLogging((hostingContext, logging) =>
                   {
                       logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                       logging.AddConsole();
                       logging.AddDebug();
                   })
                   .UseStartup<Startup>()
                   .CaptureStartupErrors(true)
                   .UseSetting(WebHostDefaults.DetailedErrorsKey, "true")
                   .UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
                 .Build();
    }
}
