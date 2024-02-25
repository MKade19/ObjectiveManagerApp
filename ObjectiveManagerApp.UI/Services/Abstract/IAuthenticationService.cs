using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IAuthenticationService
    {
        Task<PublicUserData> AuthenticateUserAsync(InternalUserData userFromUi);
        Task RegisterAsync(InternalUserData user);
    }
}
