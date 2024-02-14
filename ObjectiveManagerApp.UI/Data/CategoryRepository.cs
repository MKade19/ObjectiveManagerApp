using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;

namespace ObjectiveManagerApp.UI.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext Db;

        public CategoryRepository(ApplicationContext db)
        {
            Db = db;
        }

        public Task CreateOneAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOneAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (ApplicationContext db = Db) 
            {
                return await db.Categories.ToListAsync();
            }
        }

        public Task UpdateByIdAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IEnumerable<Category>> GetChunkAsync()
        {
            throw new NotImplementedException();
        }
    }
}
