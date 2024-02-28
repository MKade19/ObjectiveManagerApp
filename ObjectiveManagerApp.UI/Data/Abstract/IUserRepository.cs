using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IUserRepository : IRepository<InternalUserData>
    {
        Task<InternalUserData> GetByUsernameAsync(string username);
        Task<IEnumerable<PublicUserData>> GetPublicDataAsync();
        Task<PublicUserData> GetPublicDataByIdAsync(int id);
    }
}
