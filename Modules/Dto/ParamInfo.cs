namespace Sparta.Modules.Dto;

public class ParamInfo
{
    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public ParameterType Type { get; set; }

    public IModuleParameterType[] Options { get; set; } = null!;
}