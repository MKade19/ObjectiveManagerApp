using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data
{
    public class ProjectRepository : IProjectRepository
    {
        public Task CreateOneAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateByIdAsync(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
