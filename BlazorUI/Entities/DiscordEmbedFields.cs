using System.ComponentModel.DataAnnotations;

namespace Helium.BlazorUI.Entities
{
    public class DiscordEmbedFields
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Value { get; set; } = null!;

        public bool Inline { get; set; }

        public virtual DiscordEmbed DiscordEmbed { get; set; } = null!;
    }
}
