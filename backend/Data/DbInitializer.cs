using backend.Models;

namespace backend.Data
{
    public class DbInitializer
    {
        private IConfiguration _configuration;
        public DbInitializer(IConfiguration configuration) => _configuration = configuration;

        public void InitUserDb(UserDbContext context)
        {
            if (!context.Accounts.Any())
            {
                var account = new Account
                {
                    ID = new Guid().ToString(),
                    UserName = _configuration["Admin:username"] ?? "",
                    Password = _configuration["Admin:password"] ?? "",
                    AccountType = "LOCAL",
                    Email = "devilcat.admin@gmail.com",
                    Role = "Admin",
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }
    }
}
