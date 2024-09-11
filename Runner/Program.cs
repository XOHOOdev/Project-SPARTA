using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sparta.Core.DataAccess;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using Sparta.Modules.HllServerSeeding;
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
            builder.Configuration.AddConfiguration(ConfigLoader.Load());

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext<IdentityUser, ApplicationRole, string>>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(connectionString));
            builder.Services.AddScoped<Updater>();
            builder.Services.AddScoped<DiscordAccess>();
            builder.Services.AddScoped<RconDataAccess>();
            builder.Services.AddScoped<BattleMetricsDataAccess>();

            builder.Services.AddScoped<ModuleRunner>();
            builder.Services.AddScoped<DiscordRunner>();

            builder.Services.AddScoped<MapVoteModule>();
            builder.Services.AddScoped<HllServerStatusModule>();
            builder.Services.AddScoped<HllServerSeedingModule>();

            builder.Services.AddScoped<ConfigHelper>();

            builder.Services.AddSingleton<SpartaLogger>();

            var host = builder.Build();

            var serviceProvider = host.Services.CreateScope().ServiceProvider;

            serviceProvider.GetRequiredService<Updater>().Update();
            Thread.Sleep(-1);
        }
    }
}
