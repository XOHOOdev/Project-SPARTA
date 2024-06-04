using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdParameter
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string ModuleId { get; set; } = null!;

    public virtual MdModule Module { get; set; } = null!;
}
