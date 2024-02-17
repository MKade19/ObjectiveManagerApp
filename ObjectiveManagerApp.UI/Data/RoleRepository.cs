using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.Common.Models;
using ObjectiveManagerApp.UI.Data.Abstract;
using ObjectiveManagerApp.UI.Exceptions;
using System.Windows;

namespace ObjectiveManagerApp.UI.Data
{

    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext Db;
        private const string NotFoundErrorMessageName = "RoleNotFoundMessage";

        public RoleRepository(ApplicationContext db)
        {
            Db = db;
        }
        public async Task<Role> GetRoleById(int id)
        {
            using (ApplicationContext db = Db)
            {
                return await Db.Roles.FindAsync(id) ?? throw new NotFoundException((string)Application.Current.FindResource(NotFoundErrorMessageName));
            }
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            using (ApplicationContext db = Db)
            {
                return await Db.Roles.ToListAsync();
            }
        }
    }
}
