using Helium.BlazorUI.Entities;
using Helium.BlazorUI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI.Data.DiscordData
{
    public class DiscordDataService
    {
        private readonly ApplicationDbContext<IdentityUser, ApplicationRole, string> _context;

        public DiscordDataService(ApplicationDbContext<IdentityUser, ApplicationRole, string> context)
        {
            _context = context;
        }

        public List<DiscordEmbed> GetEmbeds() => _context.DC_Embeds.ToList();

        public List<DiscordGuild> GetGuilds() => _context.DC_Guilds.ToList();

        public List<DiscordAuthor> GetAuthors() => _context.DC_Authors.ToList();

        public List<DiscordModal> GetModals() => _context.DC_Modals.ToList();

        public List<DiscordMessageComponent> GetMessageComponents() => _context.DC_MessageComponents.ToList();

        public DiscordMessageComponent? GetComponent(string id) => _context.DC_MessageComponents.Find(id);

        public List<ComponentDataModel> GetComponentModels()
        {
            List<ComponentDataModel> components = new();
            var dbComponets = _context.DC_MessageComponents;

            foreach (var dbComponet in dbComponets)
            {
                components.Add(new ComponentDataModel
                {
                    Id = dbComponet.Id,
                    Label = dbComponet.Label,
                    ButtonStyle = (int)(dbComponet.ButtonStyle ?? 0),
                    EmbedTitle = string.Join(", ", _context.DC_Embeds.Where(y => y.MessageComponents.Any(z => z.Id == dbComponet.Id)).Select(x => x.Title).ToList()).Truncate(20),
                    Emote = dbComponet.Emote ?? string.Empty,
                    IsEnabled = !dbComponet.IsDisbled,
                    Url = dbComponet.Url ?? string.Empty,
                });
            }

            return components;
        }

        public DiscordEmbed? GetEmbed(long id) => _context.DC_Embeds.FirstOrDefault(x => x.Id == id);

        public void DeleteAuthor(DiscordAuthor author)
        {
            _context.Remove(author);
            _context.SaveChanges();
        }

        public void SaveAuthor(DiscordAuthor author)
        {
            if (author.Id == 0)
            {
                _context.Add(author);
            }
            else
            {
                _context.Update(author);
            }
            _context.SaveChanges();
        }

        public void DeleteEmbed(DiscordEmbed embed)
        {
            _context.DC_Embeds.Remove(embed);
            _context.SaveChanges();
        }

        public async Task SaveEmbedAsync(DiscordEmbed embed, long channelId, long selectedAuthorId)
        {
            var author = _context.DC_Authors.FirstOrDefault(x => x.Id == selectedAuthorId);
            var channel = _context.DC_Channels.FirstOrDefault(x => x.Id == Convert.ToUInt64(channelId));
            if (embed.Id == 0)
            {
                if (channel == null) return;
                embed.Updated = true;
                channel.Embeds.Add(embed);
            }
            else
            {
                _context.Update(embed);
                _context.SaveChanges();
                _context.Entry(embed).Reload();
                embed.Updated = true;
            }

            if (author != null)
            {
                author.Embeds.Add(embed);
            }
            else
            {
                var authors = _context.DC_Authors.Where(x => x.Embeds.Any(y => y.Id == embed.Id));
                if (authors.Any())
                {
                    await authors.ForEachAsync(x => x.Embeds.Remove(embed));
                }
            }

            _context.SaveChanges();
        }

        public void DeleteComponent(ComponentDataModel model)
        {
            _context.Remove(_context.DC_MessageComponents.First(x => x.Id == model.Id));
            _context.SaveChanges();
        }

        public void SaveComponent(ComponentDataModel model)
        {
            var component = _context.DC_MessageComponents.FirstOrDefault(x => x.Id == model.Id);

            if (component == null)
            {
                component = new DiscordMessageComponent { Id = Guid.NewGuid().ToString() };
                _context.Add(component);
            }

            component.ButtonStyle = (DiscordButtonStyle)model.ButtonStyle;
            component.Label = model.Label;
            component.IsDisbled = !model.IsEnabled;
            component.Emote = model.Emote;
            component.Url = model.Url;

            _context.SaveChanges();
        }
    }
}
