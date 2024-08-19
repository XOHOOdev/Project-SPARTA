namespace Sparta.Modules.Interface.ModuleParameters
{
    public abstract class ModuleParameterBase
    {
        public string Content = null!;

        public virtual int GetId()
        {
            return 0;
        }
    }
}
