using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class ModuleType
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<MdModule> MdModules { get; set; } = new List<MdModule>();
}
