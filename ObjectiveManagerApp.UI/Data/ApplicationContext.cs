using Microsoft.EntityFrameworkCore;
using ObjectiveManagerApp.Common.Models;

namespace ObjectiveManagerApp.UI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public DbSet<InternalUserData> InternalUserData { get; set; } = null!;
        public DbSet<Objective> Objectives { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<User>();

            modelBuilder.Entity<InternalUserData>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity(j => j.ToTable("UserRoles"));
        }
    }
}
