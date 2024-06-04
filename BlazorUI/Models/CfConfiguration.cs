using System;
using System.Collections.Generic;

namespace Sparta.BlazorUI.Models;

public partial class CfConfiguration
{
    public string Class { get; set; } = null!;

    public string Property { get; set; } = null!;

    public string Value { get; set; } = null!;
}
