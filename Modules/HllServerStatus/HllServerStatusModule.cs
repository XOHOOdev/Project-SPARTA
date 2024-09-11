using Discord;
using Microsoft.AspNetCore.Identity;
using Sparta.Core.DataAccess;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using Sparta.Modules.HllServerStatus.Templates;
using Sparta.Modules.Interface;
using LogSeverity = Sparta.Core.Logger.LogSeverity;

namespace Sparta.Modules.HllServerStatus
{
    public class HllServerStatusModule(DiscordAccess discord, ApplicationDbContext<IdentityUser, ApplicationRole, string> context, RconDataAccess rcon, BattleMetricsDataAccess bm, SpartaLogger logger, ConfigHelper config) : IModule
    {
        public void Run(Module module, CancellationToken token)
        {
            var serverId = ulong.Parse(module.Parameters
                .First(p => p.Name == nameof(HllServerStatusParameters.Server)).Value);

            var server = context.SV_Servers.FirstOrDefault(s => s.Id == serverId);
            context.SaveChanges();

            SendEmbed(module, GenerateEmbed(module, server));
        }

        private Embed GenerateEmbed(Module module, Server? server)
        {
            try
            {
                if (server == null)
                {
                    logger.LogMessage("The Server could not be found", LogSeverity.Warning, $"HllServerStatusModule[{module.Id}].GenerateEmbed");
                    return new EmbedBuilder().Build();
                }

                var serverEmotes = discord.GetServerEmotes(ulong.Parse(module.Parameters.First(m =>
                    m.Name == nameof(HllServerStatusParameters.DiscordChannel)).Value)).Result;

                var emotes = serverEmotes.Select(e => new KeyValuePair<string, string>(e.Name, $"<:{e.Name}:{e.Id}>")).ToArray();

                var info = rcon.GetServerInfo(server);
                var teamView = rcon.GetTeamView(server);
                var mods = rcon.GetInGameMods(server).Select(m => long.Parse(m.SteamId ?? "0")).ToList();
                var battleMetricsRank = bm.GetBattlemetricsRank(config.GetConfig("Battlemetrics", "Url") ?? "",
                    long.Parse(module.Parameters
                        .First(p => p.Name == nameof(HllServerStatusParameters.BattleMetricsId)).Value));

                if (info.Score == null) return new EmbedBuilder().Build();
                if (info.Players == null) return new EmbedBuilder().Build();

                if (StaticResources.BritainMaps.Any(x => (info.CurrentMap?.Name ?? string.Empty).StartsWith(x)))
                {
                    info.Players.RenameKey<string, int>("allied", "british");
                    info.Score.RenameKey<string, int>("allied", "british");
                }

                if (StaticResources.SovietMaps.Any(x => (info.CurrentMap?.Name ?? string.Empty).StartsWith(x)))
                {
                    info.Players.RenameKey<string, int>("allied", "soviet");
                    info.Score.RenameKey<string, int>("allied", "soviet");
                }

                var winning = info.Score.First(x => x.Value == info.Score.Max(s => s.Value)).Key;
                var losing = info.Score.First(s => s.Key != winning).Key;

                var winningEmote = emotes.First(e => e.Key == winning).Value;
                var losingEmote = emotes.First(e => e.Key == losing).Value;

                var winningTeam = winning == "axis" ? teamView.Axis : teamView.Allies;
                var losingTeam = losing == "axis" ? teamView.Axis : teamView.Allies;

                var germanTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
                var germanTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, germanTimeZone);

                var timestampFooter = new EmbedFooterBuilder()
                    .WithText($"Letzte Aktualisierung: {germanTime:HH:mm:ss}");

                return new EmbedBuilder()
                    .WithColor(new Color(43, 45, 49))
                    .WithTitle(info.Title)
                    .AddField("**Spieler**", $"{info.PlayerCount} / {info.PlayersMax}", true)
                    .AddField("**Verteilung**",
                        $"{winningEmote} {info.Players[winning]} - {info.Players[losing]} {losingEmote}",
                        true)
                    .AddField("**Karte**", $"{info.CurrentMap?.Name}", true)
                    .AddField("**Ø-Level**",
                        $"{winningEmote} {winningTeam?.GetAverageLevel() ?? 0} - {losingTeam?.GetAverageLevel() ?? 0} {losingEmote}",
                        true)
                    .AddField("**Zwischenstand**",
                        $"{winningEmote} {info.Score[winning]} - {info.Score[losing]} {losingEmote}",
                        true)
                    .AddField("**Nächste Karte**", $"{info.NextMap?.Name}", true)
                    .AddField("**Battlemetrics Rang**", battleMetricsRank, true)
                    .AddField("**VIPs**",
                        $"{winningEmote} {winningTeam?.GetVips() ?? 0} - {losingTeam?.GetVips() ?? 0} {losingEmote}",
                        true)
                    .AddField("**Spielzeit**", $"{info.CurrentMap?.StartTime.Subtract(DateTime.Now):h\\:mm} Stunden",
                        true)
                    .AddField("**Taurus Spieler**",
                        winningTeam?.GetTaurusPlayers() ?? 0 + losingTeam?.GetTaurusPlayers() ?? 0, true)
                    .AddField("**Admins**",
                        $"{winningEmote} {winningTeam?.GetPlayersInTeam(mods) ?? 0} - {losingTeam?.GetPlayersInTeam(mods) ?? 0} {losingEmote}",
                        true)
                    .AddField("**Spielende**", $"{TimeSpan.Parse(info.TimeRemaining ?? "0"):h\\:mm} Stunden", true)
                    .WithImageUrl(StaticResources.Graphics[info.CurrentMap?.Name ?? "error_uri"])
                    .WithThumbnailUrl(StaticResources.Graphics["taurus"])
                    .WithFooter(timestampFooter)
                    .Build();
            }
            catch (Exception ex)
            {
                logger.LogException(ex);
                return new EmbedBuilder()
                    .WithColor(new Color(43, 45, 49))
                    .WithThumbnailUrl(StaticResources.Graphics["taurus"])
                    .Build();
            }
        }

        private void SendEmbed(Module module, Embed embed)
        {
            var channelId = ulong.Parse(module.Parameters.First(p => p.Name == nameof(HllServerStatusParameters.DiscordChannel)).Value);

            if (module.Parameters.Any(p => p.Name == nameof(HllServerStatusParameters.DiscordMessage)))
            {
                var messageId = discord.ModifyMessageAsync(
                    channelId,
                    ulong.Parse(module.Parameters.First(p => p.Name == nameof(HllServerStatusParameters.DiscordMessage)).Value),
                    embed: embed).Result;

                if (messageId != 0) return;

                module.Parameters.Remove(module.Parameters.First(p =>
                    p.Name == nameof(HllServerStatusParameters.DiscordMessage)));

                context.SaveChanges();
            }
            else
            {
                var messageId = discord.SendMessageAsync(
                    channelId
                    , embed: embed).Result;

                if (messageId == 0) return;

                context.MD_Parameters.Add(new ModuleParameter()
                {
                    Name = nameof(HllServerStatusParameters.DiscordMessage),
                    Value = messageId.ToString(),
                    Module = module
                });

                context.SaveChanges();
            }
        }
    }
}
