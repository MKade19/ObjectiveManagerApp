using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetByUserIdAsync(int userId);
    }
}
