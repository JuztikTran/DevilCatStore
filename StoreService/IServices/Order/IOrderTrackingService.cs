using DTOs;
using DTOs.Order;

namespace StoreService.IServices.Order
{
    public interface IOrderTrackingService
    {
        IQueryable<DTOOrderTraking> GetAll();

        IQueryable<DTOOrderTraking> GetAllOfAccount(string accountID);
        Task<DTOOrderTraking> GetOne(string ID);
        Task<DTORespone> Add(DTOOrderTraking dtoOrderTraking);
        Task<DTORespone> Update(DTOOrderTraking dtoOrderTraking);
        Task<DTORespone> Delete(string ID);
    }
}
