namespace Sparta.Core.Models;

public partial class DcRole
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal GuildId { get; set; }

    public virtual DcGuild Guild { get; set; } = null!;

    public virtual ICollection<DcUser> Users { get; set; } = new List<DcUser>();

    public override bool Equals(object? obj)
    {
        if (obj is not DcRole role) return false;
        return Id == role.Id && Name == role.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
