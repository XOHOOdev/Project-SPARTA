using Sparta.Core.Helpers;
using Sparta.Core.Models;
using Sparta.DiscordService.Discord;

namespace Sparta.Runner.Runners
{
    public class DiscordRunner : IRunner
    {
        private readonly IDiscordBot _discordBot;
        private readonly SpartaDbContext _context;

        public DiscordRunner(IDiscordBot discordBot, SpartaDbContext context)
        {
            _discordBot = discordBot;
            _context = context;
        }

        public void Run(CancellationToken cancellationToken)
        {
            Update(cancellationToken);
            ServerUpdate(cancellationToken);
        }

        public void Update(CancellationToken cancellationToken)
        {
            List<DcEmbed> embeds = _context.DcEmbeds.Where(x => x.Updated).ToList();
            foreach (var embed in embeds)
            {
                _discordBot.GenerateEmbedAsync(embed).Wait(cancellationToken);
            }
            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("DiscordBot", "MessageUpdateInterval") ?? "60")), cancellationToken).ContinueWith(t => Update(cancellationToken), cancellationToken);
        }

        public void ServerUpdate(CancellationToken cancellationToken)
        {
            _discordBot.GetChannels();
            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("DiscordBot", "ChannelUpdateInterval") ?? "60")), cancellationToken).ContinueWith(t => ServerUpdate(cancellationToken), cancellationToken);
        }
    }
}
