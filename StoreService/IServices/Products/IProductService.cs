using DTOs;
using DTOs.Product;
using StoreService.Models.Product;

namespace StoreService.IServices.Products
{
    public interface IProductService
    {
        IQueryable<Product> GetAll();
        IQueryable<Product> GetOfCategory(string categoryId);
        Task<Product?> GetById(string productId);
        Task<DTORespone> Create(DTOProduct request);
        Task<DTORespone> Update(DTOProductUpdate request);
        Task<DTORespone> Delete(string productID);
    }
}
