using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcModal
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public virtual ICollection<DcMessageComponent> DcMessageComponents { get; set; } = new List<DcMessageComponent>();

    public virtual ICollection<DcModalComponent> Components { get; set; } = new List<DcModalComponent>();
}
