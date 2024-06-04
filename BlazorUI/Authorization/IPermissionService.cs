namespace Helium.BlazorUI.Authorization
{
    public interface IPermissionService
    {
        HashSet<string> GetPermissionAsync(string memberId);
    }
}
