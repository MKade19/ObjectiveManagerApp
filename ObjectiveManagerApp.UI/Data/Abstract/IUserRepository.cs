using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
