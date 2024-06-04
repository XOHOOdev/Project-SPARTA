namespace Sparta.BlazorUI.Entities;

public class DiscordMessageComponent
{
    public string Id { get; set; } = null!;

    public string Label { get; set; } = null!;

    public bool IsDisbled { get; set; }

    public DiscordButtonStyle? ButtonStyle { get; set; }

    public string? Emote { get; set; }

    public string? Url { get; set; }

    public MessageComponentAction Action { get; set; } = MessageComponentAction.None;

    public virtual DiscordChannel? ActionChannel { get; set; }

    public virtual DiscordModal? ActionModal { get; set; }

    public virtual List<DiscordEmbed> Embeds { get; } = new();
}

//From Discord.net