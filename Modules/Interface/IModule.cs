using Sparta.Core.DataAccess.DatabaseAccess.Entities;

namespace Sparta.Modules.Interface
{
    public interface IModule
    {
        void Run(Module module, CancellationToken token);
    }
}
