using DevilCatBackend.Models;

namespace DevilCatBackend.Data
{
    public class DbInitializer
    {
        public static void InitUser(UserDbContext context, Account data)
        {
            if (!context.Accounts.Any())
            {
                context.Accounts.Add(data);
                context.SaveChanges();
            }

            if (!context.Addresses.Any())
            {
                var address = new Address[] { };
                context.Addresses.AddRange(address);
                context.SaveChanges();
            }
        }

        public static void InitProduct(ProductDbContext context)
        {
            if (!context.Categories.Any())
            {
                var data = new Category[] {
                    new Category
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        Name = "T-Shirt",
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                    },
                    new Category
                    {
                        ID = Guid.NewGuid().ToString("N"),
                        Name = "Shirt",
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    }
                };
                context.Categories.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}
