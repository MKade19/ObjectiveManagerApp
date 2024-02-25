using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IObjectiveService
    {
        Task<IEnumerable<Objective>> GetObjectivesByUsername(string username);
        Task CreateOneAsync(Objective objective);
        Task EditByIdAsync(Objective objective);
        Task<Objective> GetByIdAsync(int id);
    }
}
