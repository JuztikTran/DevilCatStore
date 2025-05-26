using backend.Data;
using backend.Models;
using backend.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        Task<Product?> GetOne(string id);
        Task<DTORespone> Create(DTOProduct data);
        Task<DTORespone> Update(Product data);
        Task<DTORespone> DeleteMany(DTODelete ids);
    }

    public class ProductService : IProductService
    {
        private ProductDbContext _context;

        public ProductService(ProductDbContext context) => _context = context;

        public async Task<DTORespone> Create(DTOProduct data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request", StatusCode = 400 };
            try
            {
                var product = new Product
                {
                    ID = new Guid().ToString("N"),
                    Name = data.Name,
                    Thumbnail = data.Thumbnail,
                    Discount = data.Discount,
                    Rating = data.Rating,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = $"_id: {product.ID}", StatusCode = 201 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> DeleteMany(DTODelete ids)
        {
            try
            {
                if (ids.Ids.IsNullOrEmpty())
                    return new DTORespone { Message = "An empty list delete.", StatusCode = 400 };

                List<Product> list = new List<Product>();
                foreach (var id in ids.Ids)
                {
                    var product = await GetOne(id);
                    list.Add(product!);
                }
                _context.Products.RemoveRange(list);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = "Deleted", StatusCode = 204 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public IQueryable<Product> GetAll()
        {
            try
            {
                return _context.Products;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product?> GetOne(string id)
        {
            try
            {
                return await _context.Products.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DTORespone> Update(Product data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var product = await GetOne(data.ID);
                if (product == null)
                    return new DTORespone { Message = "Product not found.", StatusCode = 404 };
                data.UpdateAt = DateTime.Now;
                _context.Entry<Product>(product).CurrentValues.SetValues(data);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = $"_id: {data.ID}", StatusCode = 200 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }
    }
}
