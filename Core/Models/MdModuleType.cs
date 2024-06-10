using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdModuleType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MdModule> MdModules { get; set; } = new List<MdModule>();
}
