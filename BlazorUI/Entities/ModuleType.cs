namespace Sparta.BlazorUI.Entities;

public class ModuleType
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual List<Module> Modules { get; } = [];
}