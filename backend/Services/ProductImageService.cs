using backend.Data;
using backend.Models;
using backend.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services
{
    public interface IProductImageService
    {
        IQueryable<ProductImages> GetAll();
        Task<DTORespone> Create(ICollection<DTOProductImage> data);
        Task<DTORespone> Update(ProductImages data);
        Task<DTORespone> DeleteMany(DTODelete data);
    }

    public class ProductImageService : IProductImageService
    {
        public ProductDbContext _context;
        public ProductImageService(ProductDbContext context) => _context = context;
        public async Task<DTORespone> Create(ICollection<DTOProductImage> data)
        {
            if (data.IsNullOrEmpty())
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                List<ProductImages> list = new List<ProductImages>();
                foreach (var image in data)
                {
                    var product = new ProductImages
                    {
                        ID = new Guid().ToString("N"),
                        ProductID = image.ProductID,
                        Image = image.Image,
                        Position = image.Position,
                        CreateAt = DateTime.Now,
                        UpdateAt = DateTime.Now
                    };

                    list.Add(product);
                }
                _context.ProductImages.AddRange(list);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = "Created", StatusCode = 201 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public async Task<DTORespone> DeleteMany(DTODelete data)
        {
            if (data.Ids.IsNullOrEmpty())
                return new DTORespone { Message = "An empty list", StatusCode = 400 };
            try
            {
                List<ProductImages> list = new List<ProductImages>();
                foreach (var id in data.Ids)
                {
                    var image = await _context.ProductImages.FindAsync(id);
                    list.Add(image!);
                }
                _context.ProductImages.RemoveRange(list);
                await _context.SaveChangesAsync();
                return new DTORespone { Message = "Deleted", StatusCode = 204 };
            }
            catch (Exception e)
            {
                return new DTORespone { Message = e.Message, StatusCode = 500 };
            }
        }

        public IQueryable<ProductImages> GetAll()
        {
            try
            {
                return _context.ProductImages;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DTORespone> Update(ProductImages data)
        {
            if (data == null)
                return new DTORespone { Message = "Invalid data request.", StatusCode = 400 };
            try
            {
                var image = await _context.ProductImages.FindAsync(data.ID);
                if (image == null)
                    return new DTORespone { Message = "Image does not exist.", StatusCode = 404 };

                data.UpdateAt = DateTime.Now;
                _context.Entry<ProductImages>(image).CurrentValues.SetValues(data);
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
