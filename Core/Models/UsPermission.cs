using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class UsPermission
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string NormalizedName { get; set; } = null!;

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
