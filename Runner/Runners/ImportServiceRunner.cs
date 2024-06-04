using Helium.Core.Helpers;
using Helium.ImportService.Services;

namespace Helium.Runner.Runners
{
    public class ImportServiceRunner : IRunner
    {
        private readonly RconWebDataService _rconWeb;

        public ImportServiceRunner(RconWebDataService rconWeb)
        {
            _rconWeb = rconWeb;
        }

        public void Run(CancellationToken cancellationToken)
        {
            //TODO for each server
            _rconWeb.GetLogs(1, 600);
            _rconWeb.GetSessions(1, DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(7200)));
            LogImport(cancellationToken);
            BasicImport(cancellationToken);
            Thread.Sleep(-1);
        }

        private void BasicImport(CancellationToken cancellationToken)
        {
            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("DataImport", "BasicImportInterval") ?? "60")), cancellationToken).ContinueWith(t => BasicImport(cancellationToken), cancellationToken);
            _rconWeb.GetGameState(1);
            _rconWeb.GetPlayers(1);
        }

        private void LogImport(CancellationToken cancellationToken)
        {
            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("DataImport", "LogImportIntercal") ?? "60")), cancellationToken).ContinueWith(t => LogImport(cancellationToken), cancellationToken);
            _rconWeb.GetLogs(1);
            _rconWeb.GetGame(1);
            _rconWeb.GetSessions(1);
        }
    }
}
