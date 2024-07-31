using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sparta.Core.Helpers;
using Sparta.Core.Models;
using Sparta.Modules.Interface;

namespace Sparta.Runner.Runners
{
    public class ModuleRunner(SpartaDbContext context, IServiceProvider provider) : IRunner
    {
        public void Run(CancellationToken cancellationToken)
        {
            foreach (var module in context.MdModules.Include(mdModule => mdModule.Type).Where(m => m.Enabled))
            {
                if (provider.GetRequiredService(typeof(Modules.Modules).Assembly
                        .GetTypes()
                        .First(t =>
                            t.Namespace != null && t.Namespace.Contains(module.Type?.Name ?? "") && t.FullName != null &&
                            t.FullName.EndsWith("Module"))) is not IModule moduleObj) continue;

                moduleObj.Run(module, cancellationToken);
            }

            var delay = int.Parse(ConfigHelper.GetConfig("Runner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken).ContinueWith(t => Run(cancellationToken), cancellationToken);
        }
    }
}
