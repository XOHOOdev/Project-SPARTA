namespace Sparta.Core.Models;

public partial class DcGuild
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DcChannel> DcChannels { get; set; } = new List<DcChannel>();

    public virtual ICollection<DcRole> DcRoles { get; set; } = new List<DcRole>();

    public virtual ICollection<DcUser> Users { get; set; } = new List<DcUser>();

    public override bool Equals(object? obj)
    {
        if (obj is not DcGuild guild) return false;
        return Id == guild.Id && Name == guild.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
