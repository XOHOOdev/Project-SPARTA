using Sparta.Core.DataAccess;
using Sparta.Core.Dto;
using Sparta.Core.Logger;
using Sparta.Core.Models;
using Sparta.Modules.Interface;

namespace Sparta.Modules.HllServerSeeding
{
    public class HllServerSeedingModule(SpartaDbContext context, RconDataAccess rcon, SpartaLogger logger) : IModule
    {
        public void Run(MdModule module, CancellationToken token)
        {
            var serverId = long.Parse(module.MdParameters
                .First(p => p.Name == nameof(HllServerSeedingParameters.Server)).Value);

            var maxPlayers = long.Parse(module.MdParameters
                .First(p => p.Name == nameof(HllServerSeedingParameters.MaxPlayerCount)).Value);

            var seedingParameter = module.MdParameters
                .FirstOrDefault(p => p.Name == nameof(HllServerSeedingParameters.IsSeeding));

            if (seedingParameter == null)
            {
                seedingParameter = new MdParameter
                {
                    Name = nameof(HllServerSeedingParameters.IsSeeding),
                    Value = "false"
                };

                module.MdParameters.Add(seedingParameter);
                context.SaveChanges();
            }

            var currentlySeeding = bool.Parse(seedingParameter.Value);

            module.Server ??= context.SvServers.FirstOrDefault(s => s.Id == serverId);
            context.SaveChanges();

            if (module.Server == null)
            {
                logger.LogMessage("The Server could not be found", LogSeverity.Warning,
                    $"HllServerSeedingModule[{module.Id}].Run");
                return;
            }

            var serverInfo = rcon.GetServerInfo(module.Server);

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

            var teams = rcon.GetTeamView(module.Server);

            var players = (teams.Axis ?? new HllTeam()).GetPlayers()
                .Concat((teams.Allies ?? new HllTeam()).GetPlayers())
                .Concat((teams.None ?? new HllTeam()).GetPlayers());

            var messageBy = module.MdParameters.First(p => p.Name == nameof(HllServerSeedingParameters.MessageBy)).Value;

            var message = module.MdParameters.First(p => p.Name == nameof(HllServerSeedingParameters.Message)).Value;

            foreach (var player in players)
            {
                rcon.SendMessage(module.Server, player.SteamId ?? "0", message, messageBy);
            }

        }
    }
}
