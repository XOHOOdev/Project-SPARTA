using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class SvServer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int Port { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Type { get; set; }

    public virtual ICollection<MdModule> MdModules { get; set; } = new List<MdModule>();
}
