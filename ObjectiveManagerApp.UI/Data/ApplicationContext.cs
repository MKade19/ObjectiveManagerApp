using ObjectiveManagerApp.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ObjectiveManagerApp.UI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Objective> Objectives { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
    }
}
