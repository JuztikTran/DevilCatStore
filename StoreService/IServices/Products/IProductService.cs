using StoreService.Models.Product;

namespace StoreService.IServices.Products
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        IQueryable<Product> GetOfCategory(string categoryId);
        Task<Product?> GetById(string productId);
    }
}
