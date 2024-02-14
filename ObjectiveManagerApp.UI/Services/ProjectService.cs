using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;

namespace ObjectiveManagerApp.UI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync()
        {
            await foreach(var projectChunk in  _projectRepository.GetChunkAsync())
            {
                yield return projectChunk;
            }
        }
    }
}
