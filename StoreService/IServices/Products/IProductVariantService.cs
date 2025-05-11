using DTOs;
using DTOs.Product;
using StoreService.Models.Product;

namespace StoreService.IServices.Products
{
    public interface IProductVariantService
    {
        IQueryable<ProductVariant> GetAll();
        IQueryable<ProductVariant> GetOfProduct(string productId);
        Task<ProductVariant?> GetOne(string variantId);
        Task<DTORespone> Add(DTOProductVariant request);
        Task<DTORespone> Update(DTOProductVariantUpdate request);
        Task<DTORespone> Delete(string variantId);
    }
}
