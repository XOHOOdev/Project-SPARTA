using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface
{
    public interface IModuleParameters
    {
        IEnumerable<ParamInfo> AllParameters { get; set; }
    }
}
