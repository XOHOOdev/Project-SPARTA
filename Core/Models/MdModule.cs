using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdModule
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool Enabled { get; set; }

    public string? TypeId { get; set; }

    public virtual ICollection<MdParameter> MdParameters { get; set; } = new List<MdParameter>();

    public virtual ModuleType? Type { get; set; }
}
