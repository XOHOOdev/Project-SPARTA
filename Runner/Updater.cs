using Helium.Core.Helpers;
using Helium.Runner.Runners;

namespace Helium.Runner
{
    public class Updater
    {
        private readonly DiscordRunner _discordRunner;
        private readonly ImportServiceRunner _importRunner;
        private readonly Dictionary<string, CancellationTokenSource> _cancellationTokens;

        public Updater(DiscordRunner discordRunner, ImportServiceRunner importRunner)
        {
            _discordRunner = discordRunner;
            _importRunner = importRunner;
            _cancellationTokens = new Dictionary<string, CancellationTokenSource>();
        }

        public void Update()
        {
            UpdateComponent("Discord", "DiscordBot", _discordRunner);
            UpdateComponent("Import", "DataImport", _importRunner);

            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("Runner", "ImportInterval") ?? "60"))).ContinueWith(t => Update());
        }

        private void UpdateComponent(string dictionaryName, string configName, IRunner runner)
        {
            bool enabled = bool.Parse(ConfigHelper.GetConfig(configName, "enabled") ?? "false");

            if (enabled && !_cancellationTokens.ContainsKey(dictionaryName))
            {
                CancellationTokenSource cts = new();
                runner.Run(cts.Token);
                _cancellationTokens.Add(dictionaryName, cts);
            }
            else if (!enabled && _cancellationTokens.TryGetValue(dictionaryName, out CancellationTokenSource? cts))
            {
                cts.Cancel();
                cts.Dispose();
                _cancellationTokens.Remove(dictionaryName);
            }
        }
    }
}
