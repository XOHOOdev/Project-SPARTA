using Helium.Core.Models;

namespace Helium.DiscordService.Discord
{
    public interface IDiscordBot
    {
        Task GenerateMessage(ulong channelId, bool generateNew = true);

        Task GenerateEmbedAsync(DcEmbed dbEmbed);

        void GetChannels();
    }
}
