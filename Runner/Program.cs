using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sparta.Core.DataAccess;
using Sparta.Core.Helpers;
using Sparta.Core.Models;
using Sparta.Runner.Runners;

namespace Sparta.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<SpartaDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(ConfigLoader.Load().GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<Updater>();
            builder.Services.AddSingleton<DiscordAccess>();

            builder.Services.AddSingleton<ModuleRunner>();

            var host = builder.Build();

            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            serviceProvider.GetRequiredService<Updater>().Update();
            Thread.Sleep(-1);
        }
    }
}
