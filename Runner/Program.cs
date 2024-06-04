using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sparta.Core.Helpers;
using Sparta.Core.Models;
using Sparta.DiscordService.Discord;
using Sparta.Runner.Runners;

namespace Sparta.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<SpartaDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(ConfigLoader.Load().GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<IDiscordBot, DiscordBot>();
            builder.Services.AddSingleton<DiscordRunner>();
            builder.Services.AddSingleton<Updater>();

            IHost host = builder.Build();

            IServiceProvider serviceProvider = host.Services.CreateScope().ServiceProvider;

            serviceProvider.GetRequiredService<Updater>().Update();
            Thread.Sleep(-1);
        }
    }
}
