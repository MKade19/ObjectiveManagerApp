using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(TokenPayload payload);
        Task<bool> ValidateTokenAsync(string token);
    }
}
