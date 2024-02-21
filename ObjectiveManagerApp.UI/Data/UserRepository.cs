using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Exceptions;

namespace ObjectiveManagerApp.UI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext Db;
        private const string NotFoundErrorMessageName = "UserNotFoundMessage";

        public UserRepository(ApplicationContext db)
        {
            Db = db;
        }

        public async Task CreateOneAsync(InternalUserData user)
        {
            using (ApplicationContext db = Db)
            {
                await db.InternalUserData.AddAsync(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteOneAsync(InternalUserData user)
        {
            using (ApplicationContext db = Db)
            {
                await Task.Run(() =>
                {
                    db.InternalUserData.Remove(user);
                });

                await db.SaveChangesAsync();
            }
        }

        public async IAsyncEnumerable<IEnumerable<InternalUserData>> GetChunkAsync()
        {
            using (ApplicationContext db = Db)
            {
                for (int i = 0; i < db.InternalUserData.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.InternalUserData.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .ToListAsync();
                }
            }
        }

        public async Task UpdateByIdAsync(InternalUserData user)
        {
            using (ApplicationContext db = Db)
            {
                InternalUserData userFromDb = await db.InternalUserData.FindAsync(user.Id) ?? new InternalUserData();

                userFromDb.Username = user.Username;
                userFromDb.Fullname = user.Fullname;
            }
        }

        public async Task<InternalUserData> GetByUsernameAsync(string username)
        {
            using (ApplicationContext db = Db)
            {
                InternalUserData? user = await db.InternalUserData
                    .Where(u => u.Username == username)
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new NotFoundException(NotFoundErrorMessageName);
                }

                return user;
            }
        }

        public async Task<IEnumerable<InternalUserData>> GetAllAsync()
        {
            using (ApplicationContext db = Db)
            {
                return await db.InternalUserData.ToListAsync();
            }
        }

        public async Task<InternalUserData> GetByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await db.InternalUserData.FirstAsync(u => u.Id == id);
            }
        }
    }
}
