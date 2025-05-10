using DTOs;
using DTOs.Product;
using StoreService.Models.Product;

namespace StoreService.IServices.Products
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Task<Category?> GetOneById(string categoryId);
        IQueryable<Category> GetAllChildren(string parentId);
        Task<DTORespone> CreateCategory(DTOCategory category);
        Task<DTORespone> UpdateCategory(DTOCategoryUpdate category);
        Task<DTORespone> DeleteCategory(string categoryId);
    }
}
