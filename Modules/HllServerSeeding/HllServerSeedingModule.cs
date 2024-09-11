using Microsoft.AspNetCore.Identity;
using Sparta.Core.DataAccess;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Dto.Rcon;
using Sparta.Core.Logger;
using Sparta.Modules.Interface;

namespace Sparta.Modules.HllServerSeeding
{
    public class HllServerSeedingModule(ApplicationDbContext<IdentityUser, ApplicationRole, string> context, RconDataAccess rcon, SpartaLogger logger) : IModule
    {
        public void Run(Module module, CancellationToken token)
        {
            var serverId = ulong.Parse(module.Parameters
                .First(p => p.Name == nameof(HllServerSeedingParameters.Server)).Value);

            var maxPlayers = long.Parse(module.Parameters
                .First(p => p.Name == nameof(HllServerSeedingParameters.MaxPlayerCount)).Value);

            var seedingParameter = module.Parameters
                .FirstOrDefault(p => p.Name == nameof(HllServerSeedingParameters.IsSeeding));

            if (seedingParameter == null)
            {
                seedingParameter = new ModuleParameter
                {
                    Name = nameof(HllServerSeedingParameters.IsSeeding),
                    Value = "false"
                };

                module.Parameters.Add(seedingParameter);
                context.SaveChanges();
            }

            var currentlySeeding = bool.Parse(seedingParameter.Value);

            var server = context.SV_Servers.FirstOrDefault(s => s.Id == serverId);

            context.SaveChanges();

            if (server == null)
            {
                logger.LogMessage("The Server could not be found", LogSeverity.Warning,
                    $"HllServerSeedingModule[{module.Id}].Run");
                return;
            }

            var serverInfo = rcon.GetServerInfo(server);

            if (!currentlySeeding && serverInfo.PlayerCount <= maxPlayers)
            {
                logger.LogMessage("The Server begins seeding now", LogSeverity.Info,
                    $"HllServerSeedingModule[{module.Id}].Run");
                seedingParameter.Value = "true";
                context.SaveChanges();
                return;
            }

            if (!currentlySeeding || serverInfo.PlayerCount <= maxPlayers) return;

            logger.LogMessage("The Server stops seeding now", LogSeverity.Info,
                $"HllServerSeedingModule[{module.Id}].Run");
            seedingParameter.Value = "false";
            context.SaveChanges();

            var teams = rcon.GetTeamView(server);

            var players = (teams.Axis ?? new HllTeam()).GetPlayers()
                .Concat((teams.Allies ?? new HllTeam()).GetPlayers())
                .Concat((teams.None ?? new HllTeam()).GetPlayers());

            var messageBy = module.Parameters.First(p => p.Name == nameof(HllServerSeedingParameters.MessageBy)).Value;

            var message = module.Parameters.First(p => p.Name == nameof(HllServerSeedingParameters.Message)).Value;

            foreach (var player in players)
            {
                rcon.SendMessage(server, player.SteamId ?? "0", message, messageBy);
            }
        }
    }
}
