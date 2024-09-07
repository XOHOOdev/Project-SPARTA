using Sparta.Core.Helpers;
using Sparta.Core.Logger;
using Sparta.Runner.Runners;

namespace Sparta.Runner
{
    public class Updater(ModuleRunner moduleRunner, DiscordRunner discordRunner, SpartaLogger logger)
    {
        private readonly Dictionary<string, CancellationTokenSource> _cancellationTokens = [];

        public void Update()
        {
            UpdateComponent("Modules", "ModuleRunner", moduleRunner);

            UpdateComponent("Discord", "DiscordRunner", discordRunner);

            var delay = int.Parse(ConfigHelper.GetConfig("Runner", "ImportInterval") ?? "60");
            Task.Delay(TimeSpan.FromSeconds(delay)).ContinueWith(t => Update());
        }

        private void UpdateComponent(string dictionaryName, string configName, IRunner runner)
        {
            var enabled = bool.Parse(ConfigHelper.GetConfig(configName, "enabled") ?? "false");

            switch (enabled)
            {
                case true when !_cancellationTokens.ContainsKey(dictionaryName):
                    {
                        CancellationTokenSource cts = new();
                        logger.LogInfo($"Running {runner.GetType().Name}");
                        runner.Run(cts.Token);
                        _cancellationTokens.Add(dictionaryName, cts);
                        break;
                    }
                case false when _cancellationTokens.TryGetValue(dictionaryName, out var cts):
                    cts.Cancel();
                    cts.Dispose();
                    _cancellationTokens.Remove(dictionaryName);
                    break;
            }
        }
    }
}
