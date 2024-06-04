using Helium.Core.Helpers;
using Helium.Core.Models;
using Helium.DiscordService.Discord;
using Helium.ImportService.Services;
using Helium.Runner.Runners;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Helium.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<HeliumDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(ConfigLoader.Load().GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<IDiscordBot, DiscordBot>();
            builder.Services.AddSingleton<RconWebDataService>();
            builder.Services.AddSingleton<DiscordRunner>();
            builder.Services.AddSingleton<ImportServiceRunner>();
            builder.Services.AddSingleton<Updater>();

            IHost host = builder.Build();

            IServiceProvider serviceProvider = host.Services.CreateScope().ServiceProvider;

            serviceProvider.GetRequiredService<Updater>().Update();
            Thread.Sleep(-1);
        }
    }
}
