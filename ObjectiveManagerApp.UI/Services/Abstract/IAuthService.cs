using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IAuthService
    {
        Task<AuthData> LoginAsync(User user);
        Task LogoutAsync();
        Task RegisterAsync(User user);
    }
}
