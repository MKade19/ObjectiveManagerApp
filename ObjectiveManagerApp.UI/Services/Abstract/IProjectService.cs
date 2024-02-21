using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IProjectService
    {
        IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync();
        Task<IEnumerable<Project>> GetAllAsync();
        Task<IEnumerable<Project>> GetByUserIdAsync(int userId);
        Task<Project> GetByIdAsync(int id);
        Task CreateOneAsync(Project project);
        Task EditByIdAsync(Project project);
    }
}
