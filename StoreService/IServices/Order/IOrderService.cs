using DTOs;
using DTOs.Order;

namespace StoreService.IServices.Order
{
    public interface IOrderService
    {
        IQueryable<DTOOrder> GetAll();
        Task<DTOOrder> GetOne(string ID);
        IQueryable<DTOOrder> GetOneOfAccount(string accountID);
        Task<DTORespone> Add(DTOOrder order);
        Task<DTORespone> Update(DTOOrder order);
        Task<DTORespone> Delete(string ID);
    }
}
