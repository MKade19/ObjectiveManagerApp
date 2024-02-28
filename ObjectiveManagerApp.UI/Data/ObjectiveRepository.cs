using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.Common.Models;
using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.UI.Exceptions;
using System.Windows;

namespace ObjectiveManagerApp.UI.Data
{
    public class ObjectiveRepository : IObjectiveRepository
    {
        private readonly ApplicationContext Db;
        private const string ObjectiveNotFoundErrorMessageName = "ObjectiveNotFoundErrorMessage";

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

        public async Task DeleteOneAsync(Objective objective)
        {
            using (ApplicationContext db = Db)
            {
                var objectiveToRemove = await db.Objectives.FindAsync(objective.Id);

                if (objectiveToRemove != null)
                {
                    db.Entry(objectiveToRemove).State = EntityState.Detached;
                    db.Objectives.Remove(objective);
                    await db.SaveChangesAsync();
                }
            }
        }

        public Task<IEnumerable<Objective>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Objective> GetByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Objectives
                    .Include(nameof(Objective.Project))
                    .Include(nameof(Objective.Category))
                    .FirstAsync(x => x.Id == id);
            }
        }

        public IAsyncEnumerable<IEnumerable<Objective>> GetChunkAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateByIdAsync(Objective objective)
        {
            using (ApplicationContext db = Db)
            {
                Objective objectiveFromDb = await db.Objectives.FirstOrDefaultAsync(o => o.Id == objective.Id) 
                    ?? throw new NotFoundException((string)Application.Current.FindResource(ObjectiveNotFoundErrorMessageName));
                objectiveFromDb.Name = objective.Name;
                objectiveFromDb.Description = objective.Description;
                objective.UpdatedDate = DateTime.Now;
                objectiveFromDb.Deadline = objective.Deadline;
                objectiveFromDb.UserId = objective.UserId;
                objectiveFromDb.CategoryId = objective.CategoryId;

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
