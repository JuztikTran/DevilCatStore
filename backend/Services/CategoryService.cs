using backend.Data;
using backend.Models;
using backend.Shared.DTOs;

namespace backend.Services
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Task<Category?> GetOne(string id);
        Task<DTORespone> Create(DTOCategory data);
        Task<DTORespone> Update(Category data);
        Task<DTORespone> DeleteMany(DTODeleteCategory data);
    }

    public class CategoryService : ICategoryService
    {
        private ProductDbContext _context;

        public CategoryService(ProductDbContext context) => _context = context;

        public async Task<DTORespone> Create(DTOCategory data)
        {
            if (data == null)
                return new DTORespone { StatusCode = 400, Message = "Invalid data request." };
            try
            {
                var category = new Category
                {
                    ID = new Guid().ToString("N"),
                    Name = data.Name,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return new DTORespone { StatusCode = 201, Message = $"{category.ID}" };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public Task<DTORespone> DeleteMany(DTODeleteCategory data)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll()
        {
            try
            {
                return _context.Categories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category?> GetOne(string id)
        {
            try
            {
                return await _context.Categories.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DTORespone> Update(Category data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var category = await GetOne(data.ID);
                if (category == null)
                    return new DTORespone { Message = "Category does not exist.", StatusCode = 404 };
                data.UpdateAt = DateTime.Now;
                _context.Categories.Update(data);
                await _context.SaveChangesAsync();
                return new DTORespone { StatusCode = 200, Message = $"{data.ID}" };
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
