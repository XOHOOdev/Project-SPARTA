namespace Helium.BlazorUI.Data.DiscordData
{
    public class ComponentDataModel
    {
        public string Id { get; set; } = null!;

        public string Label { get; set; } = null!;

        public string EmbedTitle { get; set; } = null!;

        public bool IsEnabled { get; set; }

        public int ButtonStyle { get; set; }

        public string Emote { get; set; } = null!;

        public string Url { get; set; } = null!;
    }
}
