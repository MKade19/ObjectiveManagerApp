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
        public Task CreateOneAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Project entity)
        {
            throw new NotImplementedException();
        }

        public async IAsyncEnumerable<IEnumerable<Project>> GetChunkAsync()
        {
            using (ApplicationContext db = Db)
            {
                for (int i = 0; i < db.Projects.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.Projects.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .ToListAsync();
                }
            }
        }

        public Task UpdateByIdAsync(Project entity)
        {
            throw new NotImplementedException();
        }
    }
}
