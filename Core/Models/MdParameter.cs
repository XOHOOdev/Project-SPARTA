using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdParameter
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public long ModuleId { get; set; }

    public virtual MdModule Module { get; set; } = null!;
}
