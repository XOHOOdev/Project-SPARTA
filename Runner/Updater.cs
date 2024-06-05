using Sparta.Core.Helpers;
using Sparta.Runner.Runners;

namespace Sparta.Runner
{
    public class Updater
    {
        private readonly Dictionary<string, CancellationTokenSource> _cancellationTokens = [];

        public void Update()
        {
            //UpdateComponent("Discord", "DiscordBot", _discordRunner);

            Task.Delay(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("Runner", "ImportInterval") ?? "60"))).ContinueWith(t => Update());
        }

        private void UpdateComponent(string dictionaryName, string configName, IRunner runner)
        {
            var enabled = bool.Parse(ConfigHelper.GetConfig(configName, "enabled") ?? "false");

            switch (enabled)
            {
                case true when !_cancellationTokens.ContainsKey(dictionaryName):
                    {
                        CancellationTokenSource cts = new();
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
