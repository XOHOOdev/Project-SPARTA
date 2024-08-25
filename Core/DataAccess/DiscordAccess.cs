using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sparta.Core.Helpers;
using Sparta.Core.Models;

namespace Sparta.Core.DataAccess
{
    public class DiscordAccess
    {
        private readonly string _token;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly SpartaDbContext _dbContext;

        public DiscordAccess(SpartaDbContext dbContext)
        {
            _dbContext = dbContext;
            _token = ConfigHelper.GetConfig("DiscordBot", "DiscordToken") ?? "";

            DiscordSocketConfig discordSocketConfig = new()
            {
                GatewayIntents = GatewayIntents.All,
                AlwaysDownloadUsers = true
            };

            _client = new DiscordSocketClient(discordSocketConfig);
            var commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(commands)
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

        private async Task ModalSubmitted(SocketModal arg)
        {
            throw new NotImplementedException();
        }

        private async Task ButtonHandler(SocketMessageComponent arg)
        {
            var embed = new EmbedBuilder()
                .WithTitle("Your request is being handled")
                .WithDescription("Please stand by")
                .Build();
            await arg.RespondAsync(embed: embed, ephemeral: true);

            var user = await _dbContext.DcUsers.FindAsync((decimal)arg.User.Id);

            _dbContext.DcReceivedMessages.Add(new DcReceivedMessage
            {
                Id = arg.Id,
                Content = arg.Data.Value.IsNullOrEmpty() ? "Button Press" : arg.Data.Value,
                MessageType = 2,
                Reference = arg.Message.Id,
                UserId = arg.User.Id
            });

            await _dbContext.SaveChangesAsync();
        }

        private async Task SlashCommandHandler(SocketSlashCommand arg)
        {
            throw new NotImplementedException();
        }

        public async Task ClientReady()
        {
            //await _client.BulkOverwriteGlobalApplicationCommandsAsync(SetupHelper.BuildSlashCommands().ToArray());

            //SetupHelper.BuildStatsMessages(this);

            await _client.DownloadUsersAsync(_client.Guilds);
            Console.WriteLine($"Started as \"{_client.CurrentUser.Username}\"");
        }

        private static Task Log(LogMessage msg)
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

        public async Task<ulong> SendFileAsync(ulong channelId, FileAttachment attachment, string? content = null, Embed? embed = null)
        {
            if (_client.GetChannel(channelId) is not SocketTextChannel channel) return 0;
            var message = await channel.SendFileAsync(attachment, text: content, embed: embed);
            return message.Id;
        }

        public async Task<ulong> SendMessageAsync(ulong channelId, string? content = null, Embed? embed = null)
        {
            if (_client.GetChannel(channelId) is not SocketTextChannel channel) return 0;
            var message = await channel.SendMessageAsync(text: content, embed: embed);
            return message.Id;
        }

        public async Task<ulong> ModifyFileAsync(ulong channelId, ulong messageId, FileAttachment attachment, string? content = null, Embed? embed = null, MessageComponent? component = null)
        {
            if (_client.ConnectionState != ConnectionState.Connected) return 1;
            if (await _client.GetChannelAsync(channelId) is not SocketTextChannel channel) return 0;
            if (await channel.GetMessageAsync(messageId) is not IUserMessage message) return 0;
            try
            {
                await message.ModifyAsync(m =>
                {
                    m.Content = content;
                    m.Embed = embed;
                    m.Attachments = new Optional<IEnumerable<FileAttachment>>([attachment]);
                    m.Components = component;
                });
                return message.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task<ulong> ModifyMessageAsync(ulong channelId, ulong messageId, string? content = null, Embed? embed = null, MessageComponent? component = null)
        {
            if (_client.ConnectionState != ConnectionState.Connected) return 1;
            if (await _client.GetChannelAsync(channelId) is not SocketTextChannel channel) return 0;
            try
            {
                var message = await channel.ModifyMessageAsync(messageId, m =>
                {
                    m.Content = content;
                    m.Embed = embed;
                    m.Components = component;
                });
                return message.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 0;
            }
        }

        public async Task RespondToMessageAsync(IUserMessage? message, string? content = null, Embed? embed = null, MessageComponent? components = null)
        {
            if (message == null) return;
            await message.ReplyAsync(text: content, embed: embed, components: components);
        }

        public async Task<string> GetRoleNameAsync(ulong channelId, ulong roleId)
        {
            return await _client.GetChannelAsync(channelId) is not SocketGuildChannel channel ? string.Empty : channel.Guild.GetRole(roleId).Name;
        }

        public async Task<string> GetRoleMentionAsync(ulong channelId, ulong roleId)
        {
            return await _client.GetChannelAsync(channelId) is not SocketGuildChannel channel ? string.Empty : channel.Guild.GetRole(roleId).Mention;
        }

        public async Task<IEmbed?> GetMessageEmbed(ulong channelId, ulong messageId)
        {
            if (await _client.GetChannelAsync(channelId) is not SocketTextChannel channel) return null;
            return await channel.GetMessageAsync(messageId) is not IUserMessage message ? null : message.Embeds.First();
        }

        public async Task<IEnumerable<IReadOnlyCollection<IMessage>>> GetMessages(ulong channelId)
        {
            return await _client.GetChannelAsync(channelId) is not SocketTextChannel channel ? [] : channel.GetMessagesAsync().ToEnumerable();
        }

        public async Task DeleteMessageAsync(ulong channelId, ulong messageId)
        {
            if (await _client.GetChannelAsync(channelId) is not SocketTextChannel channel) return;
            await channel.DeleteMessageAsync(messageId);
        }

        public async Task<SocketThreadChannel?> CreateThread(ulong channelId, string threadName)
        {
            if (await _client.GetChannelAsync(channelId) is not SocketTextChannel channel) return null;
            return await channel.CreateThreadAsync(threadName, ThreadType.PrivateThread);
        }

        public async Task<bool> ChannelHasThread(ulong channelId, string threadName)
        {
            return await _client.GetChannelAsync(channelId) is SocketTextChannel channel && channel.Threads.Any(t => t.Name == threadName);
        }

        public async Task AddUserToThread(ulong threadId, ulong userId)
        {
            if (await _client.GetChannelAsync(threadId) is not SocketThreadChannel thread) return;
            var user = thread.Guild.Users.First(u => u.Id == userId);
            await thread.AddUserAsync(user);
        }

        public async Task<IReadOnlyCollection<GuildEmote>> GetServerEmotes(ulong channelId)
        {
            if (await _client.GetChannelAsync(channelId) is not IGuildChannel channel) return [];
            return await channel.Guild.GetEmotesAsync();
        }

        public DcGuild[] GetGuilds()
        {
            return _client.Guilds.Select(g => new DcGuild
            {
                Id = g.Id,
                Name = g.Name,
                Users = g.Users.Where(u => !u.IsBot).Select(u => new DcUser
                {
                    Id = u.Id,
                    Name = u.DisplayName,
                }).ToArray(),
                DcRoles = g.Roles.Select(r => new DcRole
                {
                    Id = r.Id,
                    Name = r.Name,

                }).ToArray(),
                DcChannels = g.Channels.Where(c => c is SocketTextChannel).Select(c => new DcChannel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToArray()
            }).ToArray();
        }

        public void ConnectUserRoles()
        {
            var users = _client.Guilds.SelectMany(g => g.Users).Where(u => !u.IsBot).ToArray();
            foreach (var user in users)
            {
                if (user == null) continue;
                var dbUser = _dbContext.DcUsers.Find((decimal)user.Id);
                if (dbUser == null) continue;

                foreach (var role in user.Roles)
                {
                    if (role == null) continue;
                    var dbRole = _dbContext.DcRoles.Find((decimal)role.Id);
                    if (dbRole == null) continue;

                    dbUser.Roles.Add(dbRole);
                }
            }

            _dbContext.SaveChanges();
        }
    }
}
