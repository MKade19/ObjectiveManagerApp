using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<PublicUserData>> GetAllAsync();
        Task<PublicUserData> GetByIdAsync(int id);
        Task EditByIdAsync(PublicUserData project);
        Task DeleteOneAsync(PublicUserData project);
    }
}
