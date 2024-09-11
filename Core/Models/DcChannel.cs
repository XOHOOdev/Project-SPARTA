namespace Sparta.Core.Models;

public partial class DcChannel
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal DiscordGuildId { get; set; }

    public virtual DcGuild DiscordGuild { get; set; } = null!;

    public override bool Equals(object? obj)
    {
        if (obj is not DcChannel channel) return false;
        return Id == channel.Id && Name == channel.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
