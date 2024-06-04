using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Helium.Core.Helpers;
using Helium.Core.Models;
using Helium.DiscordService.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Helium.DiscordService.Discord
{
    public class DiscordBot : IDiscordBot
    {
        private readonly string _token;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;
        private readonly HeliumDbContext _dbContext;

        public DiscordBot(HeliumDbContext dbContext)
        {
            _dbContext = dbContext;
            _token = ConfigHelper.GetConfig("DiscordBot", "DiscordToken") ?? "";

            DiscordSocketConfig discordSocketConfig = new()
            {
                GatewayIntents = GatewayIntents.All,
                AlwaysDownloadUsers = true
            };

            _client = new DiscordSocketClient(discordSocketConfig);
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            _client.Log += Log;
            _client.Ready += ClientReady;
            _client.SlashCommandExecuted += SlashCommandHandler;
            _client.ButtonExecuted += ButtonHandler;
            _client.ModalSubmitted += ModalSubmitted;

            Thread botThread = new(() =>
            {
                while (true)
                {
                    MainAsync().GetAwaiter().GetResult();
                }
            });
            botThread.Start();
        }

        public void GetChannels()
        {
            var guilds = _client.Guilds;
            foreach (SocketGuild guild in guilds)
            {
                DcGuild? dbGuild = _dbContext.DcGuilds.FirstOrDefault(x => x.Id == guild.Id);

                if (dbGuild == null)
                {
                    dbGuild = new DcGuild { Id = guild.Id, Name = guild.Name };
                    _dbContext.DcGuilds.Add(dbGuild);
                }
                foreach (SocketChannel channel in guild.Channels)
                {
                    if (!dbGuild.DcChannels.Any(x => x.Id == channel.Id))
                    {
                        dbGuild.DcChannels.Add(new DcChannel
                        {
                            Id = channel.Id,
                            Name = ((IChannel)channel).Name,
                            Type = Enum.GetName(typeof(ChannelType), channel.GetChannelType() ?? ChannelType.Voice) ?? string.Empty,
                        });
                    }
                }
            }
            _dbContext.SaveChanges();
        }

        private async Task ModalSubmitted(SocketModal modal)
        {
            //_eventPublisher.RaiseModalReceivedEvent(modal);
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            //_eventPublisher.RaiseConversationStartEvent(command, await _client.GetChannelAsync(command.ChannelId ?? 0) as IMessageChannel);
        }

        private async Task ButtonHandler(SocketMessageComponent component)
        {
            var messageComponent = _dbContext.DcMessageComponents.FirstOrDefault(x => x.Id == component.Data.CustomId);

            ModalBuilder builder = new()
            {
                Title = messageComponent?.Label,
                CustomId = Guid.NewGuid().ToString(),
            };
            builder.Components.WithTextInput("test", Guid.NewGuid().ToString());

            await component.RespondWithModalAsync(builder.Build());

            var test = component.User.GetAvatarUrl();
        }

        public async Task ClientReady()
        {
            await _client.BulkOverwriteGlobalApplicationCommandsAsync(SetupHelper.BuildSlashCommands().ToArray());

            //SetupHelper.BuildStatsMessages(this);

            await _client.DownloadUsersAsync(_client.Guilds);
        }

        public async Task GenerateMessage(ulong channelId, bool generateNew = true)
        {
            IUserMessage? userMessage = null;

            if (await _client.GetChannelAsync(channelId) is not IMessageChannel channel)
            {
                return;
            }

            IAsyncEnumerable<IMessage> messages = channel.GetMessagesAsync(limit: 50).Flatten();

            if (!generateNew)
            {
                bool dbContainsMessages = _dbContext.DcStatsChannels.Any();

                await foreach (IMessage message in messages)
                {
                    if (message is IUserMessage
                        && message.Author.Id == _client.CurrentUser.Id
                        && (!dbContainsMessages || _dbContext.DcStatsChannels.FirstOrDefault(row => row.MessageId == message.Id.ToString()) != null))
                    {
                        userMessage = message as IUserMessage;
                        break;
                    }
                }
            }

            if (userMessage == null)
            {
                Embed placeholderEmbed = new EmbedBuilder()
                    .WithTitle("Placeholder")
                    .WithDescription("Nur ein Platzhalter.\nBitte auf refresh warten.")
                    .Build();
                userMessage = await channel.SendMessageAsync(embed: placeholderEmbed);
            }

            DcStatsChannel? statsMessage = _dbContext.DcStatsChannels.FirstOrDefault(row => row.MessageId == userMessage.Id.ToString()) ?? new DcStatsChannel { MessageId = userMessage.Id.ToString() };
            _dbContext.Update(statsMessage);
            _dbContext.SaveChanges();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, _token);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public async Task GenerateEmbedAsync(DcEmbed dbEmbed)
        {
            _dbContext.Entry(dbEmbed).Reload();
            if (_client.GetChannel(Convert.ToUInt64(dbEmbed.DiscordChannelId)) is not IMessageChannel channel) return;

            Embed embed = InteractionHelper.BuildEmbed(dbEmbed);

            MessageComponent component = InteractionHelper.BuildComponent(dbEmbed);

            if (dbEmbed.MessageId == 0)
            {
                dbEmbed.Updated = false;
                dbEmbed.MessageId = Convert.ToInt64((await channel.SendMessageAsync(embed: embed, components: component)).Id);
                _dbContext.SaveChanges();
                return;
            }

            try
            {
                await channel.ModifyMessageAsync(Convert.ToUInt64(dbEmbed.MessageId), mes =>
                {
                    mes.Embed = embed;
                    mes.Components = component;
                });
                dbEmbed.Updated = false;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                dbEmbed.MessageId = 0;
                _dbContext.SaveChanges();
                await GenerateEmbedAsync(dbEmbed);
            }
        }
    }
}
