using DTOs;
using DTOs.Order;
using StoreService.Models.Order;

namespace StoreService.IServices.Order
{
    public interface ICartService
    {
        IQueryable<Cart> GetAll();
        IQueryable<Cart> GetOfUser(string accountId);
        Task<Cart> GetOne(string Id);
        Task<DTORespone> Add(DTOCart request);
        Task<DTORespone> Update(DTOCart request);
        Task<DTORespone> Delete(string Id);
    }
}
