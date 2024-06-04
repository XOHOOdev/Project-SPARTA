namespace Sparta.BlazorUI.Entities;

public class ModuleParameter
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public virtual Module Module { get; set; } = null!;
}