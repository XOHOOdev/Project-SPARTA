namespace Helium.Runner.Runners
{
    public interface IRunner
    {
        public void Run(CancellationToken cancellationToken);
    }
}
