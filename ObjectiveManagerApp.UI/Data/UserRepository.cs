using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Constants;
using ObjectiveManagerApp.UI.Data.Abstract;

namespace ObjectiveManagerApp.UI.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext Db;
        private const string NotFoundErrorMessageName = "RoleNotFoundMessage";

        public UserRepository(ApplicationContext db)
        {
            Db = db;
        }

        public async Task CreateOneAsync(User user)
        {
            using (ApplicationContext db = Db)
            {
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteOneAsync(User user)
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

        public async IAsyncEnumerable<IEnumerable<User>> GetChunkAsync()
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

        public async Task UpdateByIdAsync(User user)
        {
            using (ApplicationContext db = Db)
            {
                User userFromDb = await db.Users.FindAsync(user.Id) ?? new User();

                userFromDb.Username = user.Username;
                userFromDb.Fullname = user.Fullname;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (ApplicationContext db = Db)
            {
                return await db.Users.Where(u => u.Username == username).FirstAsync();
            }
        }

    }
}
