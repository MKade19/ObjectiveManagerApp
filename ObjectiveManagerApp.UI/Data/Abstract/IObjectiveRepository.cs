using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IObjectiveRepository : IRepository<Objective>
    {
        Task<IEnumerable<Objective>> GetByUserIdAsync(int userInt);
    }
}
