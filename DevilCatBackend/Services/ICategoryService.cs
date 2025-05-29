using DevilCatBackend.Data;
using DevilCatBackend.Models;

namespace DevilCatBackend.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> Get();
    }

    public class CategoryService : ICategoryService
    {
        private ProductDbContext _context;

        public CategoryService(ProductDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Get()
        {
            return _context.Categories;
        }
    }
}
