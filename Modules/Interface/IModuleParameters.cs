using Sparta.Core.Dto.Modules;

namespace Sparta.Modules.Interface
{
    public interface IModuleParameters
    {
        IEnumerable<ParamInfo> AllParameters { get; set; }
    }
}
