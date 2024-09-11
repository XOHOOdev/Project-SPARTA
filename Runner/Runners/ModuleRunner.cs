using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using Sparta.Modules.Interface;

namespace Sparta.Runner.Runners
{
    public class ModuleRunner(ApplicationDbContext<IdentityUser, ApplicationRole, string> context, IServiceProvider provider, SpartaLogger logger, ConfigHelper config) : IRunner
    {
        public void Run(CancellationToken cancellationToken)
        {
            var modules = context.MD_Modules.Include(mdModule => mdModule.Type).Where(m => m.Enabled).ToArray();

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

            var delay = int.Parse(config.GetConfig("ModuleRunner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay), cancellationToken).ContinueWith(t => Run(cancellationToken), cancellationToken);
        }
    }
}
