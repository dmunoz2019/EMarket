using BusinessLogic.Data;
using BusinessLogic.Data.Load;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API;
public class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build(); 
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
               var context = services.GetRequiredService<MarketDbContext>();
               await  context.Database.MigrateAsync();
               await LoadDbContextData.LoadDataAsync(context, loggerFactory);

                var usermanager = services.GetRequiredService<UserManager<User>>();
                var identityContext = services.GetRequiredService<SecurityDbContext>();
                await identityContext.Database.MigrateAsync();
                 await SecurityDbContextData.SeedUserAsync(usermanager);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occured during migration");
            }
        }
        host.Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
}
