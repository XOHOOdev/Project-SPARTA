using Sparta.Core.Models;

namespace Sparta.DiscordService.Discord
{
    public interface IDiscordBot
    {
        Task GenerateMessage(ulong channelId, bool generateNew = true);

        Task GenerateEmbedAsync(DcEmbed dbEmbed);

        void GetChannels();
    }
}
