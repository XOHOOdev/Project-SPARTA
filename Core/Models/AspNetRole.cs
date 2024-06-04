using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NormalizedName { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<UsPermission> Permissions { get; set; } = new List<UsPermission>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
