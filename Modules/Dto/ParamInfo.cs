namespace Sparta.Modules.Dto;

public class ParamInfo
{
    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Type { get; set; }

    public IDiscordType[] Options { get; set; } = null!;
}