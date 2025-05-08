using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using StoreService.Models.User;

namespace StoreService.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Profile>().ToTable("Profile");
            modelBuilder.Entity<Address>().ToTable("Address");
        }

    }
}
