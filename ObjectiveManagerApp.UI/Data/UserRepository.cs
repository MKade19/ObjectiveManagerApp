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
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteOneAsync(InternalUserData user)
        {
            using (ApplicationContext db = Db)
            {
                await Task.Run(() =>
                {
                    db.Users.Remove(user);
                });

                await db.SaveChangesAsync();
            }
        }

        public async IAsyncEnumerable<IEnumerable<InternalUserData>> GetChunkAsync()
        {
            using (ApplicationContext db = Db)
            {
                for (int i = 0; i < db.Users.Count(); i += DataConstants.RecordsLimit)
                {
                    yield return await db.Users.Skip(i)
                        .Take(DataConstants.RecordsLimit)
                        .ToListAsync();
                }
            }
        }

        public async Task UpdateByIdAsync(InternalUserData user)
        {
            using (ApplicationContext db = Db)
            {
                InternalUserData userFromDb = await db.Users.FindAsync(user.Id) ?? new InternalUserData();

                userFromDb.Username = user.Username;
                userFromDb.Fullname = user.Fullname;
            }
        }

        public async Task<InternalUserData> GetByUsernameAsync(string username)
        {
            using (ApplicationContext db = Db)
            {
                InternalUserData? user = await db.Users
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
                return await db.Users.ToListAsync();
            }
        }

        public async Task<InternalUserData> GetByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Users.FirstAsync(u => u.Id == id);
            }
        }

        public async Task<PublicUserData> GetPublicDataByIdAsync(int id)
        {
            using (ApplicationContext db = Db)
            {
                var query = from u in db.Users
                            where u.Id == id
                            select new PublicUserData(u.Id, u.Username, u.Fullname, u.Roles);

                return await query.FirstAsync();
            }
        }

        public async Task<IEnumerable<PublicUserData>> GetPublicDataAsync()
        {
            using (ApplicationContext db = Db)
            {
                var query = from u in db.Users
                            select new PublicUserData(u.Id, u.Username, u.Fullname, u.Roles);

                return await query.ToListAsync();
            }
        }
    }
}
