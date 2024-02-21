using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;
using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.UI.Exceptions;

namespace ObjectiveManagerApp.UI.Data
{
    public class ObjectiveRepository : IObjectiveRepository
    {
        private readonly ApplicationContext Db;

        public ObjectiveRepository(ApplicationContext db)
        {
            Db = db;
        }

        public async Task CreateOneAsync(Objective objective)
        {
            using (ApplicationContext db = Db)
            {
                objective.CreatedDate = DateTime.Now;
                objective.UpdatedDate = DateTime.Now;

                await db.Objectives.AddAsync(objective);
                await db.SaveChangesAsync();
            }
        }

        public Task DeleteOneAsync(Objective objective)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Objective>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Objective> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IEnumerable<Objective>> GetChunkAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByIdAsync(Objective objective)
        {
            using (ApplicationContext db = Db)
            {
                //ADD ERROR MESSAGE
                Objective objectiveFromDb = await db.Objectives.FirstOrDefaultAsync() ?? throw new NotFoundException("");
                objectiveFromDb.Name = objective.Name;
                objectiveFromDb.Description = objective.Description;
                objective.UpdatedDate = DateTime.Now;
                objectiveFromDb.Deadline = objective.Deadline;
                objectiveFromDb.UserId = objective.UserId;

                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Objective>> GetByUserIdAsync(int userId)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Objectives
                    .Where(o => o.UserId == userId)
                    .Include(nameof(Objective.Project))
                    .Include(nameof(Objective.Category))
                    .ToListAsync();
            }
        }
    }
}
