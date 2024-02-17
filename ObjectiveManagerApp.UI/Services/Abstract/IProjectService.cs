using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Services.Abstract
{
    public interface IProjectService
    {
        IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync();
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
    }
}
