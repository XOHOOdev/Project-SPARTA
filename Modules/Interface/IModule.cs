using Sparta.Core.Models;

namespace Sparta.Modules.Interface
{
    public interface IModule
    {
        void Run(MdModule module, CancellationToken token);
    }
}
