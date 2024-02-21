using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Services.Abstract;
using System.Security.AccessControl;

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

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
        {
            return await _projectRepository.GetByUserIdAsync(userId);
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task CreateOneAsync(Project project)
        {
            await _projectRepository.CreateOneAsync(project);
        }

        public async Task EditByIdAsync(Project project)
        {
            await _projectRepository.UpdateByIdAsync(project);
        }
    }
}
