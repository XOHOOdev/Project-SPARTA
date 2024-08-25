using Sparta.Core.DataAccess;
using Sparta.Core.Helpers;
using Sparta.Core.Models;

namespace Sparta.Runner.Runners
{
    public class DiscordRunner(SpartaDbContext context, DiscordAccess discord) : IRunner
    {
        public async Task UpdateAsync(CancellationToken cancellationToken)
        {
            var guilds = discord.GetGuilds();
            if (guilds.Length > 0)
            {
                foreach (var guild in context.DcGuilds)
                {
                    context.Remove(guild);
                }

                foreach (var user in context.DcUsers)
                {
                    context.Remove(user);
                }

                foreach (var role in context.DcRoles)
                {
                    context.Remove(role);
                }

                foreach (var channel in context.DcChannels)
                {
                    context.Remove(channel);
                }

                await context.SaveChangesAsync(cancellationToken);

                context.DcGuilds.AddRange(guilds);
                await context.SaveChangesAsync(cancellationToken);

                discord.ConnectUserRoles();
            }

        }

        public void Run(CancellationToken cancellationToken)
        {
            try
            {
                UpdateAsync(cancellationToken).Wait(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            var delay = int.Parse(ConfigHelper.GetConfig("DiscordRunner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken).ContinueWith(t => Run(cancellationToken), cancellationToken);
        }
    }
}
