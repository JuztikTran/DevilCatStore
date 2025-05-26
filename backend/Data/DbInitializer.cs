using backend.Models;

namespace backend.Data
{
    public class DbInitializer
    {
        public static void InitUserDb(UserDbContext context, string username, string password)
        {
            string temp = "";
            if (!context.Accounts.Any())
            {
                var account = new Account
                {
                    ID = new Guid().ToString(),
                    UserName = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    AccountType = "LOCAL",
                    Email = "devilcat.admin@gmail.com",
                    Role = "Admin",
                    FacebookId = "",
                    GoogleId = "",
                    IsActive = true,
                    IsBanned = false,
                    Reason = "",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                temp = account.ID;
                context.Accounts.Add(account);
                context.SaveChanges();
            }

            if (!context.Profiles.Any())
            {
                var profile = new Profile
                {
                    ID = temp,
                    FirstName = "Admin",
                    LastName = "Store",
                    Gender = "Other",
                    Avatar = "",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };
            }
        }

        public static void InitProductDb(ProductDbContext context)
        {
            if (!context.Categories.Any())
            {
                var category = new Category[] { };
                context.Categories.AddRange(category);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                var products = new Product[] { };
                context.Products.AddRange(products);
                context.SaveChanges();
            }

            if (!context.ProductVarriants.Any())
            {
                var varriants = new ProductVarriant[] { };
                context.ProductVarriants.AddRange(varriants);
                context.SaveChanges();
            }

            if (!context.ProductImages.Any())
            {
                var images = new ProductImages[] { };
                context.ProductImages.AddRange(images);
                context.SaveChanges();
            }
        }
    }
}
