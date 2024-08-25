using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sparta.Core.DataAccess;
using Sparta.Core.Helpers;
using Sparta.Core.Models;
using Sparta.Modules.HllServerStatus;
using Sparta.Modules.MapVote;
using Sparta.Runner.Runners;

namespace Sparta.Runner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddDbContext<SpartaDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(ConfigLoader.Load().GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            builder.Services.AddScoped<Updater>();
            builder.Services.AddScoped<DiscordAccess>();
            builder.Services.AddScoped<RconDataAccess>();
            builder.Services.AddScoped<BattleMetricsDataAccess>();

            builder.Services.AddScoped<ModuleRunner>();
            builder.Services.AddScoped<DiscordRunner>();

            builder.Services.AddScoped<MapVoteModule>();
            builder.Services.AddScoped<HllServerStatusModule>();

            var host = builder.Build();

            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            serviceProvider.GetRequiredService<Updater>().Update();
            Thread.Sleep(-1);
        }
    }
}
