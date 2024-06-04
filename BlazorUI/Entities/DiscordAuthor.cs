namespace Helium.BlazorUI.Entities
{
    public class DiscordAuthor
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string? IconUrl { get; set; }

        public string? Url { get; set; }

        public virtual List<DiscordEmbed> Embeds { get; } = new();
    }
}
