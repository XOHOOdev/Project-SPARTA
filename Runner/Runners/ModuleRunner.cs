using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using Sparta.Core.Models;
using Sparta.Modules.Interface;

namespace Sparta.Runner.Runners
{
    public class ModuleRunner(SpartaDbContext context, IServiceProvider provider, SpartaLogger logger) : IRunner
    {
        public void Run(CancellationToken cancellationToken)
        {
            var modules = context.MdModules.Include(mdModule => mdModule.Type).Where(m => m.Enabled).ToArray();

            logger.LogDebug($"Found {modules.Length} module(s) [{string.Join(", ", modules.Select(m => $"{m.Type.Name}({m.Id})]"))}");

            foreach (var module in modules)
            {
                if (provider.GetRequiredService(typeof(Modules.Modules).Assembly
                        .GetTypes()
                        .First(t =>
                            t.Namespace != null && t.Namespace.Contains(module.Type.Name) && t.FullName != null &&
                            t.FullName.EndsWith("Module"))) is not IModule moduleObj) continue;

                try
                {
                    logger.LogVerbose($"Running Module \"{module.Type.Name}({module.Id})\"");
                    moduleObj.Run(module, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                }
            }

            var delay = int.Parse(ConfigHelper.GetConfig("ModuleRunner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken).ContinueWith(t => Run(cancellationToken), cancellationToken);
        }
    }
}
