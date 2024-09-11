using Sparta.Core.DataAccess;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;

namespace Sparta.Runner.Runners
{
    public class DiscordRunner(DiscordAccess discord, SpartaLogger logger, ConfigHelper config) : IRunner
    {
        public void UpdateAsync(CancellationToken cancellationToken)
        {
            discord.UpdateGuilds();
        }

        public void Run(CancellationToken cancellationToken)
        {
            try
            {
                UpdateAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
            }

            var delay = int.Parse(config.GetConfig("DiscordRunner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken).ContinueWith(t => Run(cancellationToken), cancellationToken);
        }
    }
}
