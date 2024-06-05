using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcReceivedMessage
{
    public decimal Id { get; set; }

    public decimal Reference { get; set; }

    public int MessageType { get; set; }

    public string Content { get; set; } = null!;
}
