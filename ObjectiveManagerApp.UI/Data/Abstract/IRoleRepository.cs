using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data.Abstract
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleById(int id);
        Task<IEnumerable<Role>> GetAllAsync();
    }
}
