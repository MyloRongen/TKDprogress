using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TKDprogress_DAL.Entities;

namespace TKDprogress_DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movement> Movements { get; set; }
        public DbSet<Tul> Tuls { get; set; }
        public DbSet<Terminology> Terminologies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TulMovement> TulMovements { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<UserTul> UserTuls { get; set; }
    }
}