using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class LgLogMessage
{
    public long Id { get; set; }

    public int Severity { get; set; }

    public string Source { get; set; } = null!;

    public string ShortSource { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string ShortMessage { get; set; } = null!;

    public DateTimeOffset Time { get; set; }
}
