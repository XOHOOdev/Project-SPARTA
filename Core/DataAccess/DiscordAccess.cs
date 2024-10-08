﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using LogMessage = Discord.LogMessage;

namespace Sparta.Core.DataAccess
{
    public class DiscordAccess
    {
        private readonly string _token;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly ApplicationDbContext<IdentityUser, ApplicationRole, string> _dbContext;
        private readonly SpartaLogger _logger;

        private bool _isReady = false;

        public DiscordAccess(ApplicationDbContext<IdentityUser, ApplicationRole, string> dbContext, SpartaLogger logger, ConfigHelper config)
        {
            _dbContext = dbContext;
            _token = config.GetConfig("DiscordBot", "DiscordToken") ?? "";
            _logger = logger;

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

            var user = await _dbContext.DC_Users.FindAsync((decimal)arg.User.Id);

            _dbContext.DC_ReceivedMessages.Add(new DiscordReceivedMessage()
            {
                Id = arg.Id,
                Content = arg.Data.Value.IsNullOrEmpty() ? "Button Press" : arg.Data.Value,
                MessageType = DiscordMessageType.Component,
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
            _logger.LogInfo($"Started Discord Bot as \"{_client.CurrentUser.Username}\"");

            _isReady = true;
        }

        private Task Log(LogMessage msg)
        {
            if (msg.Exception != null)
            {
                _logger.LogException(msg.Exception);
            }
            else
            {
                _logger.LogMessage(msg.Message, (Logger.LogSeverity)msg.Severity);
            }

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
                _logger.LogException(ex);
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
                _logger.LogException(ex);
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

        public async Task UpdateGuilds(CancellationToken ct)
        {
            if (!_isReady) return;

            await _client.DownloadUsersAsync(_client.Guilds);

            var dcGuilds = _client.Guilds.Select(g => new DiscordGuild()
            {
                Id = g.Id,
                Name = g.Name,
            }).ToArray();
            var dbGuilds = _dbContext.DC_Guilds.ToList();

            var guildsToAdd = dcGuilds.Except(dbGuilds).ToArray();
            var guildsToRemove = dbGuilds.Except(dcGuilds).ToArray();

            _dbContext.DC_Guilds.AddRange(guildsToAdd);
            _dbContext.DC_Guilds.RemoveRange(guildsToRemove);

            dbGuilds.AddRange(guildsToAdd);

            var dcChannels = _client.Guilds.SelectMany(g => g.Channels).Where(c => c is SocketTextChannel).Select(c => new DiscordChannel()
            {
                Id = c.Id,
                Name = c.Name,
                DiscordGuild = dbGuilds.First(g => g.Id == c.Guild.Id)
            }).ToArray();
            var dbChannels = _dbContext.DC_Channels.ToArray();

            var channelsToAdd = dcChannels.Except(dbChannels).ToArray();
            var channelsToRemove = dbChannels.Except(dcChannels).ToArray();

            _dbContext.DC_Channels.AddRange(channelsToAdd);
            _dbContext.DC_Channels.RemoveRange(channelsToRemove);

            var dcRoles = _client.Guilds.SelectMany(g => g.Roles).Select(r => new DiscordRole()
            {
                Id = r.Id,
                Name = r.Name,
                Guild = dbGuilds.First(g => g.Id == r.Guild.Id)
            }).ToArray();
            var dbRoles = _dbContext.DC_Roles.ToList();

            var rolesToAdd = dcRoles.Except(dbRoles).ToArray();
            var rolesToRemove = dbRoles.Except(dcRoles).ToArray();

            _dbContext.DC_Roles.AddRange(rolesToAdd);
            _dbContext.DC_Roles.RemoveRange(rolesToRemove);

            dbRoles.AddRange(rolesToAdd);

            var dcUsers = _client.Guilds.SelectMany(g => g.Users).DistinctBy(u => u.Id).Where(u => !u.IsBot).Select(u => new DiscordUser()
            {
                Id = u.Id,
                Name = u.DisplayName,
                DiscordGuilds = u.MutualGuilds.Select(g => dbGuilds.FirstOrDefault(gdb => gdb.Id == g.Id)).OfType<DiscordGuild>().ToList(),
            }).ToArray();
            var dbUsers = _dbContext.DC_Users.ToList();

            var usersToAdd = dcUsers.Except(dbUsers).ToArray();
            var usersToRemove = dbUsers.Except(dbUsers).ToArray();

            _dbContext.DC_Users.AddRange(usersToAdd);
            _dbContext.DC_Users.RemoveRange(usersToRemove);

            dbUsers.AddRange(usersToAdd);

            foreach (var user in dbUsers)
            {
                var roles = _client.GetUser(user.Id).MutualGuilds
                    .SelectMany(g => g.Users)
                    .Where(u => u.Id == user.Id)
                    .SelectMany(u => u.Roles)
                    .ToArray();

                dcRoles = roles.Select(r => dbRoles.First(dr => dr.Id == r.Id)).ToArray();

                rolesToAdd = user.Roles.Except(dcRoles).ToArray();
                rolesToRemove = user.Roles.Except(dcRoles).ToArray();

                user.Roles.AddRange(rolesToAdd);
                foreach (var discordRole in rolesToRemove)
                {
                    user.Roles.Remove(discordRole);
                }
            }

            await _dbContext.SaveChangesAsync(ct);
        }
    }
}