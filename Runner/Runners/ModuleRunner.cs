using Sparta.Core.DataAccess;

namespace Sparta.Runner.Runners
{
    public class ModuleRunner(DiscordAccess discord) : IRunner
    {
        private DiscordAccess _discordAccess = discord;

        public void Run(CancellationToken cancellationToken)
        {

        }
    }
}
