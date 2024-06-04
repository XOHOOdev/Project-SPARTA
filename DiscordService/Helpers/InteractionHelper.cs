using Discord;
using Helium.Core.Models;

namespace Helium.DiscordService.Helpers
{
    internal static class InteractionHelper
    {
        public static Embed BuildEmbed(DcEmbed dbEmbed)
        {
            EmbedBuilder embedBuilder = new();
            if (dbEmbed.DiscordAuthor != null)
            {
                embedBuilder.WithAuthor(new EmbedAuthorBuilder
                {
                    Name = dbEmbed.DiscordAuthor.Name,
                    Url = dbEmbed.DiscordAuthor.Url,
                    IconUrl = dbEmbed.DiscordAuthor.IconUrl
                });
            }
            if (dbEmbed.ColorArgb != 0)
            {
                System.Drawing.Color color = System.Drawing.Color.FromArgb(dbEmbed.ColorArgb);
                embedBuilder.WithColor(new Color(color.R, color.G, color.B));
            }
            if (dbEmbed.Description != null)
            {
                embedBuilder.WithDescription(dbEmbed.Description);
            }
            foreach (var field in dbEmbed.DcEmbedFields)
            {
                embedBuilder.AddField(field.Name, field.Value, field.Inline);
            }
            if (dbEmbed.FooterText != null || dbEmbed.FooterIconUrl != null)
            {
                embedBuilder.WithFooter(new EmbedFooterBuilder { Text = dbEmbed.FooterText, IconUrl = dbEmbed.FooterIconUrl });
            }
            if (dbEmbed.ImageUrl != null)
            {
                embedBuilder.WithImageUrl(dbEmbed.ImageUrl);
            }
            if (dbEmbed.ThumbnailUrl != null)
            {
                embedBuilder.WithThumbnailUrl(dbEmbed.ThumbnailUrl);
            }
            if (dbEmbed.Title != null)
            {
                embedBuilder.WithTitle(dbEmbed.Title);
            }
            if (dbEmbed.Url != null)
            {
                embedBuilder.WithUrl(dbEmbed.Url);
            }

            return embedBuilder.Build();
        }

        public static MessageComponent BuildComponent(DcEmbed dbEmbed)
        {
            ComponentBuilder? componentBuilder = null;

            if (dbEmbed.MessageComponents.Count > 0)
            {
                componentBuilder = new ComponentBuilder();
                foreach (var component in dbEmbed.MessageComponents)
                {
                    var buttonBuilder = new ButtonBuilder
                    {
                        CustomId = component.ButtonStyle == null || (ButtonStyle)component.ButtonStyle != ButtonStyle.Link ? component.Id : null,
                        Emote = component.Emote != null ? (component.Emote.StartsWith('<') ? Emote.Parse(component.Emote) : new Emoji(component.Emote)) : null,
                        IsDisabled = component.IsDisbled,
                        Label = component.Label,
                        Style = component.ButtonStyle != null ? (ButtonStyle)component.ButtonStyle : ButtonStyle.Primary,
                        Url = component.ButtonStyle != null && (ButtonStyle)component.ButtonStyle == ButtonStyle.Link ? component.Url : null,
                    };
                    componentBuilder.WithButton(buttonBuilder);
                }
            }

            return (componentBuilder ?? new ComponentBuilder()).Build();
        }
    }
}
