using DTOs;
using DTOs.Product;
using StoreService.Models.Product;

namespace StoreService.IServices.Products
{
    public interface IProductImageService
    {
        IQueryable<ProductImage> GetAll();
        Task<ProductImage?> GetOne(string productId);
        Task<IEnumerable<ProductImage>> GetAllOfProduct(string productId);
        Task<DTORespone> Add(DTOProductImage request);
        Task<DTORespone> AddList(List<DTOProductImage> request);
        Task<DTORespone> Update(DTOProductImageUpdate request);
        Task<DTORespone> Delete(string imageId);
    }
}
