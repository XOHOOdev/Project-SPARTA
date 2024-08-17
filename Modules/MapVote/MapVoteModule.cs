using Discord;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Sparta.Core.DataAccess;
using Sparta.Core.Models;
using Sparta.Modules.Interface;
using Sparta.Modules.MapVote.Dto;
using System.Numerics;
using System.Text;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;
using Path = System.IO.Path;

namespace Sparta.Modules.MapVote
{
    public class MapVoteModule(DiscordAccess discord, SpartaDbContext context) : IModule
    {
        public void Run(MdModule module, CancellationToken token)
        {
            var mdParameter = module.MdParameters
                .FirstOrDefault(p => p.Name == nameof(MapVoteParameters.Votes));

            var votes = GetVotes(mdParameter) ?? GenerateVoteList().ToList();

            if (module.MdParameters.All(p => p.Name != nameof(MapVoteParameters.Sides)))
            {
                context.Add(new MdParameter
                {
                    Module = module,
                    Name = nameof(MapVoteParameters.Sides),
                    Value = Convert.ToBoolean(new Random().Next(0, 1)).ToString()
                });
                context.SaveChanges();
            }

            var lastBan = GetMessagesAndVote(votes, module);

            const string imageName = "MapVoteState.jpg";
            var embed = GenerateEmbed(discord, module, votes, lastBan, imageName);
            var image = GenerateImage(module, votes);

            if (lastBan == null && GetVoteCount(votes) > 0) return;
            SendFile(module, embed, new FileAttachment(stream: image, fileName: imageName));
            SaveVotes(votes, mdParameter, module);
        }

        private static List<MapVoteItem>? GetVotes(MdParameter? parameter)
        {
            return parameter == null ? null : JsonConvert.DeserializeObject<List<MapVoteItem>>(parameter.Value);
        }

        private void SaveVotes(List<MapVoteItem> votes, MdParameter? parameter, MdModule module)
        {
            parameter ??= new MdParameter { Module = module, Name = nameof(MapVoteParameters.Votes) };

            parameter.Value = JsonConvert.SerializeObject(votes);
            context.Update(parameter);
            context.SaveChanges();
        }

        private static int GetVoteCount(List<MapVoteItem> votes) => votes.Count(v => v.State > 0);

        private static Embed GenerateEmbed(DiscordAccess discord, MdModule module, List<MapVoteItem> votes, string? lastBan, string imageName)
        {
            Embed embed;

            var channelId = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);
            var messageId = ulong.Parse(module.MdParameters.FirstOrDefault(p => p.Name == nameof(MapVoteParameters.DiscordMessage))?.Value ?? "0");
            var sides = bool.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Sides)).Value);
            var coinFlipRoleId = ulong.Parse(module.MdParameters.First(p => p.Name == (sides ? nameof(MapVoteParameters.Team1) : nameof(MapVoteParameters.Team2))).Value);
            var currentVoter = GetCurrentVoter(discord, votes, module);

            var mapVoteMessageBuilder = new StringBuilder(messageId == 0 ? "" : discord.GetMessageEmbed(channelId, messageId).Result?.Description);

            if (mapVoteMessageBuilder.Length == 0)
            {
                var mention = discord.GetRoleMentionAsync(channelId, coinFlipRoleId).Result;
                if (mention.IsNullOrEmpty()) return new EmbedBuilder().Build();
                mapVoteMessageBuilder.AppendLine($"{mention} won the coinflip and may start voting");
            }

            if (lastBan != null)
            {
                mapVoteMessageBuilder.AppendLine();
                mapVoteMessageBuilder.AppendLine(
                    $"{currentVoter.RoleMention} banned {lastBan}");

                votes.First(v => v.MapName == lastBan).State = currentVoter.State;
            }

            if (GetVoteCount(votes) == votes.Count - 1)
            {
                var order = Convert.ToInt32(sides);

                var roles = new ulong[]
                {
                    ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Team1)).Value),
                    ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Team2)).Value)
                }.Select(r => discord.GetRoleMentionAsync(channelId, r).Result).ToArray();

                embed = new EmbedBuilder()
                    .WithTitle("Map Vote")
                    .WithDescription(mapVoteMessageBuilder.ToString())
                    .WithImageUrl($"attachment://{imageName}")
                    .WithFields(
                        new EmbedFieldBuilder()
                            .WithName("Map")
                            .WithValue(votes.First(v => v.State == MapVoteState.Unknown).MapName)
                            .WithIsInline(true)
                        , new EmbedFieldBuilder()
                            .WithName("Allies")
                            .WithValue(roles[0])
                            .WithIsInline(true)
                        , new EmbedFieldBuilder()
                            .WithName("Axis")
                            .WithValue(roles[1])
                            .WithIsInline(true))
                    .Build();
            }
            else
            {
                embed = new EmbedBuilder()
                    .WithTitle("Map Vote")
                    .WithDescription(mapVoteMessageBuilder.ToString())
                    .WithImageUrl($"attachment://{imageName}")
                    .Build();
            }

            return embed;
        }

        private IEnumerable<string> GetTeamNames(MdModule module)
        {
            var channelId = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);
            return module.MdParameters
                .Where(p => p.Name is nameof(MapVoteParameters.Team1) or nameof(MapVoteParameters.Team2))
                .Select(p => discord.GetRoleNameAsync(channelId, ulong.Parse(p.Value)).Result);
        }

        private static MapVoter GetCurrentVoter(DiscordAccess discord, List<MapVoteItem> votes, MdModule module)
        {
            var sides = bool.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Sides)).Value);
            var channelId = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);

            var voteCount = (GetVoteCount(votes) + (sides ? 0 : 1));
            var switchCondition = voteCount % 2;
            switch (switchCondition)
            {
                default:
                    var roleId1 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Team1)).Value);
                    var userId1 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Rep1)).Value);
                    return new MapVoter
                    {
                        RoleId = roleId1,
                        RoleMention = discord.GetRoleMentionAsync(channelId, roleId1).Result,
                        State = MapVoteState.Team1,
                        UserId = userId1
                    };
                case 1:
                    var roleId2 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Team2)).Value);
                    var userId2 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Rep2)).Value);
                    return new MapVoter
                    {
                        RoleId = roleId2,
                        RoleMention = discord.GetRoleMentionAsync(channelId, roleId2).Result,
                        State = MapVoteState.Team2,
                        UserId = userId2
                    };
            }
        }

        private MemoryStream GenerateImage(MdModule module, List<MapVoteItem> votes)
        {
            var teamNames = GetTeamNames(module).ToArray();
            const int imgWidth = 4000;
            const int imgHeight = 3000;
            const int margin = 20;

            var cellHeight = (imgHeight - margin * 2) / (votes.Count + 1.5f);
            var currentHeight = cellHeight * 1.5f;

            var fonts = new FontCollection();
            var family = fonts.Add(Path.Combine("Fonts", "Proxima Nova Font.otf"));
            var fontBold = family.CreateFont(currentHeight, FontStyle.Bold);
            var fontReg = family.CreateFont(cellHeight, FontStyle.Regular);

            Image image = new Image<Rgba32>(imgWidth, imgHeight);
            image.Mutate(i => i.Fill(new SolidBrush(Color.White)));

            image.Mutate(img => img.Fill(
                Color.Gray,
                new RectangleF(
                    new PointF(margin, margin)
                    , new SizeF(imgWidth - margin * 2, currentHeight))
            ));

            image.Mutate(i => i.DrawText(teamNames[0], fontBold, Color.Black, new Point(margin, margin)));
            image.Mutate(i => i.DrawText(new RichTextOptions(fontBold)
            {
                Origin = new Vector2(imgWidth / 2f, margin),
                HorizontalAlignment = HorizontalAlignment.Center
            }, "Maps", Color.Black));
            image.Mutate(i => i.DrawText(new RichTextOptions(fontBold)
            {
                Origin = new Vector2(imgWidth - margin, margin),
                HorizontalAlignment = HorizontalAlignment.Right
            }, teamNames[1], Color.Black));

            image.Mutate(i => i.DrawLine(new SolidBrush(Color.Black), 3, new PointF(margin, margin), new PointF(imgWidth - margin, margin)));
            image.Mutate(i => i.DrawLine(new SolidBrush(Color.Black), 3, new PointF(margin, currentHeight + margin), new PointF(imgWidth - margin, currentHeight + margin)));
            image.Mutate(i => i.DrawLine(new SolidBrush(Color.Black), 3, new PointF(margin, imgHeight - margin), new PointF(imgWidth - margin, imgHeight - margin)));
            image.Mutate(i => i.DrawLine(new SolidBrush(Color.Black), 3, new PointF(margin, margin), new PointF(margin, imgHeight - margin)));
            image.Mutate(i => i.DrawLine(new SolidBrush(Color.Black), 3, new PointF(imgWidth - margin, margin), new PointF(imgWidth - margin, imgHeight - margin)));

            for (var i = 0; i < votes.Count; i++)
            {
                if (votes[i].State != MapVoteState.Unknown)
                {
                    image.Mutate(img => img.Fill(
                        Color.Red,
                        new RectangleF(
                            new PointF((imgWidth - margin * 2) * 0.25f + margin, margin + currentHeight + i * cellHeight)
                            , new SizeF((imgWidth - margin * 2) * 0.5f, cellHeight))
                        ));
                    image.Mutate(img => img.Fill(
                        Color.Red,
                        new RectangleF(
                            new PointF((imgWidth - margin * 2) * (votes[i].State == MapVoteState.Team1 ? 0.0f : 0.75f) + margin, margin + currentHeight + i * cellHeight)
                            , new SizeF((imgWidth - margin * 2) * 0.25f, cellHeight))
                    ));
                    image.Mutate(img => img.Fill(
                        Color.Orange,
                        new RectangleF(
                            new PointF((imgWidth - margin * 2) * (votes[i].State == MapVoteState.Team1 ? 0.75f : 0.0f) + margin, margin + currentHeight + i * cellHeight)
                            , new SizeF((imgWidth - margin * 2) * 0.25f, cellHeight))
                    ));
                }
                else if (GetVoteCount(votes) + 1 == votes.Count)
                {
                    image.Mutate(img => img.Fill(
                        Color.LimeGreen,
                        new RectangleF(
                            new PointF(margin, margin + currentHeight + i * cellHeight)
                            , new SizeF(imgWidth - margin * 2, cellHeight))
                    ));
                }

                image.Mutate(img => img.DrawLine(
                    new SolidBrush(Color.Black),
                    3,
                    new PointF(margin, margin + currentHeight + i * cellHeight),
                    new PointF(imgWidth - margin, margin + currentHeight + i * cellHeight)));
                image.Mutate(img => img.DrawText(new RichTextOptions(fontReg)
                {
                    Origin = new Vector2(margin, margin + currentHeight + i * cellHeight),
                    HorizontalAlignment = HorizontalAlignment.Left
                }, teamNames[0], Color.Black));
                image.Mutate(img => img.DrawText(new RichTextOptions(fontReg)
                {
                    Origin = new Vector2(imgWidth / 2f, margin + currentHeight + i * cellHeight),
                    HorizontalAlignment = HorizontalAlignment.Center
                }, votes[i].MapName, Color.Black));
                image.Mutate(img => img.DrawText(new RichTextOptions(fontReg)
                {
                    Origin = new Vector2(imgWidth - margin, margin + currentHeight + i * cellHeight),
                    HorizontalAlignment = HorizontalAlignment.Right
                }, teamNames[1], Color.Black));
            }

            var memStream = new MemoryStream();
            image.SaveAsJpeg(memStream);

            return memStream;
        }

        private void SendFile(MdModule module, Embed embed, FileAttachment attachment)
        {
            const string threadName = "match-orga";

            var channelId = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);
            if (!discord.ChannelHasThread(channelId, threadName).Result)
            {
                var thread = discord.CreateThread(channelId, threadName).Result;
                if (thread == null) return;

                var rep1 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Rep1)).Value);
                var rep2 = ulong.Parse(module.MdParameters.First(p => p.Name == nameof(MapVoteParameters.Rep2)).Value);

                discord.AddUserToThread(thread.Id, rep1).Wait();
                discord.AddUserToThread(thread.Id, rep2).Wait();
            }

            if (module.MdParameters.Any(p => p.Name == nameof(MapVoteParameters.DiscordMessage)))
            {
                var messageId = discord.ModifyFileAsync(
                    channelId
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
                try
                {
                    var messageId = discord.SendFileAsync(
                        channelId
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static IEnumerable<MapVoteItem> GenerateVoteList()
        {
            dynamic json = JObject.Parse(File.ReadAllText(Path.Combine("MapVote", "MapVoteConfig.json")));
            var mapList = (json.AvailableMaps as JArray)?.ToObject<List<string>>();

            return mapList == null ? [] : mapList.Select(m => new MapVoteItem { MapName = m, State = MapVoteState.Unknown });
        }

        private string? GetMessagesAndVote(List<MapVoteItem> votes, MdModule module)
        {
            var userId = GetCurrentVoter(discord, votes, module).UserId;

            var channelId = ulong.Parse(module.MdParameters
                .First(p => p.Name == nameof(MapVoteParameters.DiscordChannel)).Value);

            var messages = discord
                .GetMessages(channelId).Result.SelectMany(c => c).ToArray();

            if (messages.Length == 0) return null;

            var respondedMessages = messages.Select(m => new[]
                {
                    m, messages.FirstOrDefault(msg => msg.Reference != null && msg.Reference.MessageId.Value == m.Id)
                })
                .Where(m2 => m2[1] != null).ToArray();

            foreach (var respondedMessage in respondedMessages)
            {
                discord.DeleteMessageAsync(channelId, respondedMessage[0]?.Id ?? 0).Wait();
                discord.DeleteMessageAsync(channelId, respondedMessage[1]?.Id ?? 0).Wait();
            }

            var relevantMessages = messages.Where(m => userId.Equals(m.Author.Id)).ToArray();

            foreach (var wrongMessage in messages.Where(m => !m.Author.IsBot && !relevantMessages.Contains(m)))
            {
                if (respondedMessages.Select(m => m[0]).Contains(wrongMessage)) continue;
                discord.RespondToMessageAsync(wrongMessage as IUserMessage, "Please wait for your turn").Wait();
            }

            if (relevantMessages.LastOrDefault() is not { } message) return null;

            var messageContent = message.Content;
            if (!messageContent.StartsWith("ban ", StringComparison.OrdinalIgnoreCase)) return null;

            messageContent = messageContent.Remove(0, 4);
            var messageArr = messageContent.ToLowerInvariant().ToCharArray().Select(c => (int)c).ToArray();

            var availableVotes = GetVoteCount(votes) + 1 < votes.Count ? votes.Where(v => v.State == MapVoteState.Unknown).ToList() : [];
            var mapMaxLength = availableVotes.Select(v => v.MapName).Max(s => s.Length);
            var bestGuess = availableVotes.Select(v =>
                new KeyValuePair<MapVoteItem, int>(v, DamerauLevenshteinDistance(messageArr, v.MapName.ToLowerInvariant().ToCharArray().Select(c => (int)c).ToArray(),
                mapMaxLength))).MinBy(v => v.Value);

            if (bestGuess.Value > messageContent.Length / 3)
            {
                discord.RespondToMessageAsync(message as IUserMessage, "Your vote couldn't be processed.").Wait();
                return null;
            }

            discord.DeleteMessageAsync(channelId, message.Id).Wait();
            return bestGuess.Key.MapName;
        }

        private static int DamerauLevenshteinDistance(int[] source, int[] target, int threshold)
        {
            var length1 = source.Length;
            var length2 = target.Length;

            // Return trivial case - difference in string lengths exceeds threshhold
            if (Math.Abs(length1 - length2) > threshold) { return int.MaxValue; }

            // Ensure arrays [i] / length1 use shorter length 
            if (length1 > length2)
            {
                Swap(ref target, ref source);
                Swap(ref length1, ref length2);
            }

            var maxi = length1;
            var maxj = length2;

            var dCurrent = new int[maxi + 1];
            var dMinus1 = new int[maxi + 1];
            var dMinus2 = new int[maxi + 1];

            for (var i = 0; i <= maxi; i++) { dCurrent[i] = i; }

            var jm1 = 0;

            for (var j = 1; j <= maxj; j++)
            {

                // Rotate
                var dSwap = dMinus2;
                dMinus2 = dMinus1;
                dMinus1 = dCurrent;
                dCurrent = dSwap;

                // Initialize
                var minDistance = int.MaxValue;
                dCurrent[0] = j;
                var im1 = 0;
                var im2 = -1;

                for (var i = 1; i <= maxi; i++)
                {

                    var cost = source[im1] == target[jm1] ? 0 : 1;

                    var del = dCurrent[im1] + 1;
                    var ins = dMinus1[i] + 1;
                    var sub = dMinus1[im1] + cost;

                    //Fastest execution for min value of 3 integers
                    var min = (del > ins) ? (ins > sub ? sub : ins) : (del > sub ? sub : del);

                    if (i > 1 && j > 1 && source[im2] == target[jm1] && source[im1] == target[j - 2])
                        min = Math.Min(min, dMinus2[im2] + cost);

                    dCurrent[i] = min;
                    if (min < minDistance) { minDistance = min; }
                    im1++;
                    im2++;
                }
                jm1++;
                if (minDistance > threshold) { return int.MaxValue; }
            }

            var result = dCurrent[maxi];
            return (result > threshold) ? int.MaxValue : result;
        }

        private static void Swap<T>(ref T arg1, ref T arg2) => (arg1, arg2) = (arg2, arg1);
    }
}