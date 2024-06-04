namespace Sparta.BlazorUI.Entities;

public class DiscordModal
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual List<DiscordModalComponent> Components { get; } = new();

    public virtual List<DiscordMessageComponent> MessageComponents { get; } = new();
}