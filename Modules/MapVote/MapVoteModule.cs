using CoreHtmlToImage;
using Discord;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sparta.Core.DataAccess;
using Sparta.Core.Models;
using Sparta.Modules.Interface;
using Sparta.Modules.MapVote.Dto;
using System.Text;

namespace Sparta.Modules.MapVote
{
    public class MapVoteModule(DiscordAccess discord, SpartaDbContext context) : IModule
    {
        private readonly HtmlConverter _converter = new();

        public void Run(MdModule module, CancellationToken token)
        {
            var votes = module.MdParameters.Any(p => p.Name == nameof(MapVoteParameters.Votes))
                ? JsonConvert.DeserializeObject<List<MapVoteItem>>(module.MdParameters
                    .First(p => p.Name == nameof(MapVoteParameters.Votes)).Value) ?? GenerateVoteList().ToList()
                : GenerateVoteList().ToList();

            GetMessagesAndVote(votes, module);

            var image = GenerateImage(module, votes);
            var imageName = $"MapVoteState-{votes.Count(v => v.State > 0)}.jpg";

            SendFile(module, GenerateEmbed("", imageName), new FileAttachment(stream: image, fileName: imageName));
        }

        private static Embed GenerateEmbed(string description, string imageName)
        {
            var embed = new EmbedBuilder()
                .WithTitle("Map Vote")
                .WithDescription(description)
                .WithImageUrl($"attachment://{imageName}")
                .Build();
            return embed;
        }

        private IEnumerable<string> GetTeamNames(MdModule module)
        {
            var channelId = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);
            return module.MdParameters
                .Where(p => p.Name == nameof(MapVoteParameters.Team1) || p.Name == nameof(MapVoteParameters.Team2))
                .Select(p => discord.GetRoleNameAsync(channelId, ulong.Parse(p.Value)).Result);
        }

        private MemoryStream GenerateImage(MdModule module, List<MapVoteItem> votes)
        {
            var banStateString = File.ReadAllText(@"MapVote\Templates\VoteStateTemplate.html");
            var tableRowBuilder = new StringBuilder();
            var teamNames = GetTeamNames(module);

            var votedMapsCount = votes.Count(v => v.State > 0);

            foreach (var vote in votes)
            {
                tableRowBuilder.AppendLine(vote.State switch
                {
                    MapVoteState.Unknown => "<tr>" +
                                            $"    <td class=\"not_chosen\">State</div>" +
                                            $"    <td class=\"mapname, not_chosen\">{vote.MapName}</td>" +
                                            $"    <td class=\"not_chosen\">State</div>",
                    MapVoteState.Team1 => "<tr>" +
                                          $"    <td class=\"chosen_by_you\">State</div>" +
                                          $"    <td class=\"mapname, chosen_by_you\">{vote.MapName}</td>" +
                                          $"    <td class=\"chosen_by_opponent\">State</div>",
                    MapVoteState.Team2 => "<tr>" +
                                          $"    <td class=\"chosen_by_opponent\">State</div>" +
                                          $"    <td class=\"mapname, chosen_by_you\">{vote.MapName}</td>" +
                                          $"    <td class=\"chosen_by_you\">State</div>",
                    _ => ""
                });
            }
            banStateString = banStateString.Replace("BODY_HERE", tableRowBuilder.ToString());
            banStateString = banStateString.Replace("TEAM1_NAME", teamNames.First());
            banStateString = banStateString.Replace("TEAM2_NAME", teamNames.Last());
            var bytes = _converter.FromHtmlString(banStateString);

            return new MemoryStream(bytes);
        }

        private void SendFile(MdModule module, Embed embed, FileAttachment attachment)
        {
            if (module.MdParameters.Any(p => p.Name == nameof(MapVoteParameters.DiscordMessage)))
            {
                var messageId = discord.ModifyFileAsync(
                    ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value)
                    , ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordMessage)).Value)
                    , embed: embed
                    , attachment: attachment).Result;

                if (messageId != 0) return;

                module.MdParameters.Remove(module.MdParameters.First(p =>
                    p.Name == nameof(MapVoteParameters.DiscordMessage)));

                context.SaveChanges();
            }
            else
            {
                var messageId = discord.SendFileAsync(
                    ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value)
                    , embed: embed
                    , attachment: attachment).Result;

                if (messageId == 0) return;

                module.MdParameters.Add(new MdParameter
                {
                    Name = nameof(MapVoteParameters.DiscordMessage),
                    Value = messageId.ToString()
                });

                context.SaveChanges();
            }
        }

        private IEnumerable<MapVoteItem> GenerateVoteList()
        {
            dynamic json = JObject.Parse(File.ReadAllText(@"MapVote\MapVoteConfig.json"));
            var mapList = (json.AvailableMaps as JArray)?.ToObject<List<string>>();

            return mapList == null ? [] : mapList.Select(m => new MapVoteItem { MapName = m, State = MapVoteState.Unknown });
        }

        private void GetMessagesAndVote(List<MapVoteItem> votes, MdModule module)
        {
            var userIds = module.MdParameters.Where(p =>
                p.Name is nameof(MapVoteParameters.Rep1) or nameof(MapVoteParameters.Rep2)).Select(p => ulong.Parse(p.Value));

            var messages = discord
                .GetMessages(ulong.Parse(module.MdParameters
                    .First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value)).Result;

            var enumerable = messages as IReadOnlyCollection<IMessage>[] ?? messages.ToArray();
            if (!enumerable.Any()) return;
            var relevantMessages = enumerable.SelectMany(c => c).Where(m => userIds.Contains(m.Author.Id));
        }
    }
}