using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IUserRepository : IRepository<InternalUserData>
    {
        Task<InternalUserData> GetUserByUsernameAsync(string username);
    }
}
