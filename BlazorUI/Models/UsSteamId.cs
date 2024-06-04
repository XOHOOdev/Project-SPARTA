using System;
using System.Collections.Generic;

namespace Sparta.BlazorUI.Models;

public partial class UsSteamId
{
    public string UserId { get; set; } = null!;

    public long SteamId { get; set; }
}
