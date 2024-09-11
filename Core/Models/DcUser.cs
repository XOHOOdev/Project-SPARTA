namespace Sparta.Core.Models;

public partial class DcUser
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DcGuild> DiscordGuilds { get; set; } = new List<DcGuild>();

    public virtual ICollection<DcRole> Roles { get; set; } = new List<DcRole>();

    public override bool Equals(object? obj)
    {
        if (obj is not DcUser user) return false;
        return Id == user.Id && Name == user.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
