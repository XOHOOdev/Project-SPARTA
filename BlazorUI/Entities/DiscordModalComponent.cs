namespace Sparta.BlazorUI.Entities;

public class DiscordModalComponent
{
    public string Id { get; set; } = null!;

    public string Label { get; set; } = null!;

    public TextInputStyle InputStyle { get; set; } = TextInputStyle.Short;

    public string? Placeholder { get; set; }

    public int? MinLength { get; set; }

    public int? MaxLength { get; set; }

    public bool? Required { get; set; }

    public string? DefaultValue { get; set; }

    public virtual List<DiscordModal> DiscordModals { get; } = new();
}

public enum TextInputStyle
{
    //
    // Summary:
    //     Intended for short, single-line text.
    Short = 1,

    //
    // Summary:
    //     Intended for longer or multiline text.
    Paragraph
}