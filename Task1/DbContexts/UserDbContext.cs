using System.Data.Entity;
using Task1.Model;

namespace Task1.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("UserDb")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<UserDbContext>());
        }

        public DbSet<User> User { get; set; }
    }
}
