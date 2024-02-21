using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using ObjectiveManagerApp.UI.Exceptions;

namespace ObjectiveManagerApp.UI.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationContext Db;

        public ProjectRepository(ApplicationContext db)
        {
            Db = db;
        }
        public async Task CreateOneAsync(Project project)
        {
            using (ApplicationContext db = Db)
            {
                project.CreatedDate = DateTime.Now;
                project.UpdatedDate = DateTime.Now;

                await db.Projects.AddAsync(project);
                await db.SaveChangesAsync();
            }
        }

        public Task DeleteOneAsync(Project project)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            using (ApplicationContext db = Db)
            {
                return await db.Projects.Include(nameof(Project.Objectives)).ToListAsync();
            }
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Projects.Include(nameof(Project.Objectives)).FirstAsync(p => p.Id == id);
            }
        }

        public async Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Projects
                    .Where(p => p.ManagerId == userId)
                    .ToListAsync();
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
                        .Include(nameof(Project.Objectives))
                        .ToListAsync();
                }
            }
        }

        public async Task UpdateByIdAsync(Project project)
        {
            using (ApplicationContext db = Db)
            {
                //ADD ERROR MESSAGE
                Project projectFromDb = await db.Projects.FirstOrDefaultAsync(p => p.Id == project.Id) ?? throw new NotFoundException("");
                projectFromDb.Name = project.Name;
                projectFromDb.Description = project.Description;
                projectFromDb.UpdatedDate = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }
    }
}
