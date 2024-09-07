using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class MdModule
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Enabled { get; set; }

    public long TypeId { get; set; }

    public decimal? ServerId { get; set; }

    public virtual ICollection<MdParameter> MdParameters { get; set; } = new List<MdParameter>();

    public virtual SvServer? Server { get; set; }

    public virtual MdModuleType Type { get; set; } = null!;
}
