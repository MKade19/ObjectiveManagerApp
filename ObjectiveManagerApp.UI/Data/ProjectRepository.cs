using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using Microsoft.EntityFrameworkCore;

namespace ObjectiveManagerApp.UI.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationContext Db;

        public ProjectRepository(ApplicationContext db)
        {
            Db = db;
        }
        public Task CreateOneAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            using (ApplicationContext db = Db)
            {
                return await db.Projects.ToListAsync();
            }
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Projects.FirstAsync(p => p.Id == id);
            }
        }

        public async IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync()
        {
            using (ApplicationContext db = Db)
            {
                for (int i = 0; i < db.Projects.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.Projects.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .OrderBy(p => p.Id)
                        .ToListAsync();
                }
            }
        }

        public Task UpdateByIdAsync(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
