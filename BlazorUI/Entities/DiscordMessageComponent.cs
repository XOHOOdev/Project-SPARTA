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
public enum DiscordButtonStyle
{
    //
    // Summary:
    //     A Blurple button
    Primary = 1,

    //
    // Summary:
    //     A Grey (or gray) button
    Secondary,

    //
    // Summary:
    //     A Green button
    Success,

    //
    // Summary:
    //     A Red button
    Danger,

    //
    // Summary:
    //     A Discord.ButtonStyle.Secondary button with a little popup box indicating that
    //     this button is a link.
    Link
}

public enum MessageComponentAction
{
    None = 0,

    CreateChannel = 1,

    OpenModal = 2
}