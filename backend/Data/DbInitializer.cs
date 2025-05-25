using backend.Models;

namespace backend.Data
{
    public class DbInitializer
    {
        public static void InitUserDb(UserDbContext context, string username, string password)
        {
            if (!context.Accounts.Any())
            {
                var account = new Account
                {
                    ID = new Guid().ToString(),
                    UserName = username,
                    Password = password,
                    AccountType = "LOCAL",
                    Email = "devilcat.admin@gmail.com",
                    Role = "Admin",
                    FacebookId = "",
                    GoogleId = "",
                    IsActive = true,
                    IsBanned = false,
                    Reason = ""
                };

                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }
    }
}
