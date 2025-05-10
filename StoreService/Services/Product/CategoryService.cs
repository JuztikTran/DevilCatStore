using DTOs;
using DTOs.Product;
using Microsoft.EntityFrameworkCore;
using StoreService.Data;
using StoreService.IServices.Products;
using StoreService.Models.Product;

namespace StoreService.Services.Product
{
    public class CategoryService : ICategoryService
    {
        private ProductDBContext productDBContext;

        public CategoryService(ProductDBContext context) => productDBContext = context;

        public async Task<DTORespone> CreateCategory(DTOCategory category)
        {
            if (category == null)
                return new DTORespone { IsSuccess = false, Message = "Invalid data request." };

            try
            {
                var checkData = await this.productDBContext.Categories
                    .AnyAsync(c => c.Name.ToLower().Equals(category.Name.ToLower()));

                if (checkData) throw new Exception("The category already exist.");

                Category data = new Category
                {
                    Name = category.Name,
                    ParentID = category.ParentID
                };

                this.productDBContext.Add(data);
                await this.productDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = true, Message = "Create category success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }

        }

        public async Task<DTORespone> DeleteCategory(string categoryId)
        {
            try
            {
                var category = await this.GetOneById(categoryId);
                if (category == null) throw new Exception("Category does not exist.");

                this.productDBContext.Categories.Remove(category);
                await this.GetOneById(categoryId);

                return new DTORespone { IsSuccess = true, Message = "Delete category success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }

            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll()
        {
            return productDBContext.Categories
                .Select(c => new Category
                {
                    ID = c.ID,
                    Name = c.Name,
                    ParentID = c.ParentID,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt,
                });
        }

        public IQueryable<Category> GetAllChildren(string parentId)
        {
            return productDBContext.Categories
                .Where(c => c.ParentID != null && c.ParentID.Equals(parentId));
        }

        public async Task<Category?> GetOneById(string categoryId)
        {
            return await this.productDBContext.Categories.FirstOrDefaultAsync(c => c.ID.Equals(categoryId));
        }

        public async Task<DTORespone> UpdateCategory(DTOCategoryUpdate category)
        {
            if (category == null)
                return new DTORespone { IsSuccess = false, Message = "Invalid data request." };

            try
            {
                var data = await this.GetOneById(category.ID);
                if (data == null) throw new Exception("Category does not exist.");

                data.Name = category.Name;
                data.ParentID = category.ParentID;
                data.UpdateAt = DateTime.Now;

                this.productDBContext.Categories.Update(data);
                await this.productDBContext.SaveChangesAsync();

                return new DTORespone { IsSuccess = false, Message = "Update category success." };
            }
            catch (Exception e)
            {
                return new DTORespone { IsSuccess = false, Message = e.Message };
            }

        }
    }
}
