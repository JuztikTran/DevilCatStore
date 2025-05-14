using DTOs;
using DTOs.Order;

namespace StoreService.IServices.Order
{
    public interface IOrderDetailService
    {
        IQueryable<DTOOrderDetails> GetAll();
        Task<DTOOrderDetails> GetOfOrder(string orderID);
        Task<DTOOrderDetails> GetOne(string id);
        Task<DTORespone> Add(DTOOrderDetails dtoOrderDetails);
        Task<DTORespone> Update(DTOOrderDetails dtoOrderDetails);
        Task<DTORespone> Delete(string id);
    }
}
