using Discord;
using Helium.Core.Helpers;
using Helium.DiscordService.Discord;

namespace Helium.DiscordService.Helpers
{
    public class SetupHelper
    {
        public static List<ApplicationCommandProperties> BuildSlashCommands()
        {
            List<ApplicationCommandProperties> applicationCommandProperties = new();

            SlashCommandBuilder globalHelpCommand = new SlashCommandBuilder()
                .WithName("help")
                .WithDescription("Finde heraus wie der Bot funktioniert");
            applicationCommandProperties.Add(globalHelpCommand.Build());

            return applicationCommandProperties;
        }

        public static void BuildStatsMessages(IDiscordBot discordBot)
        {
            ulong channelId = ulong.Parse(ConfigHelper.GetConfig("DiscordBot", "DiscordStatsChannel") ?? "0");

            discordBot.GenerateMessage(channelId, false);
        }
    }
}
