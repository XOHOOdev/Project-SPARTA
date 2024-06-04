using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Helium.BlazorUI.Entities
{
    public class DiscordEmbed
    {
        public long Id { get; set; }

        public bool Updated { get; set; }

        public long MessageId { get; set; }

        public int ColorArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        [NotMapped]
        public string ColorHtml
        {
            get => ColorTranslator.ToHtml(Color);
            set => Color = ColorTranslator.FromHtml(value);
        }

        [NotMapped]
        public Color Color { get; set; }

        public string? Description { get; set; }

        public string? FooterIconUrl { get; set; }

        public string? FooterText { get; set; }

        public string? ImageUrl { get; set; }

        public string? ThumbnailUrl { get; set; }

        public string? Title { get; set; }

        public string? Url { get; set; }

        public virtual List<DiscordEmbedFields> Fields { get; } = new();

        public virtual List<DiscordMessageComponent> MessageComponents { get; } = new();

        public virtual DiscordChannel DiscordChannel { get; } = new();
    }
}
