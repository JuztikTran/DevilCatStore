using StoreService.Models.User;

namespace StoreService.Data
{
    public class DbInitializer
    {
        public static void InitializeUserDB(UserDBContext context)
        {
            string temp = "";

            if (!context.Accounts.Any())
            {
                var account = new Account
                {
                    UserName = "AdminStore",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin@123"),
                    Email = "DevilCatStore.admin@gmail.com",
                    Role = Models.Role.ADMIN,
                    AccountType = "LOCAL",
                    IsActive = true,
                    IsBan = false,
                    RefreshToken = null,                   
                };
                temp = account.ID;

                context.Accounts.Add(account);
                context.SaveChanges();
            }

            if (!context.Addresses.Any()) {
                var address = new Address[] { };
                context.Addresses.AddRange(address);
                context.SaveChanges();
            }

            if (!context.Profiles.Any()) {
                var profile = new Profile
                {
                    AccountID = temp,
                    FirstName = "Cris",
                    LastName = "Phan",
                    Gender = Models.Gender.Male,
                };
                context.Profiles.AddRange(profile);
                context.SaveChanges();
            }
        }
    }
}
