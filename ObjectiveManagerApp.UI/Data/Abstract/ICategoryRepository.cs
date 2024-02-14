using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
