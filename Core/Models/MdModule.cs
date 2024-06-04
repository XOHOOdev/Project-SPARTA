using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdModule
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<MdParameter> MdParameters { get; set; } = new List<MdParameter>();
}
