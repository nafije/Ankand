using Ankand.Models;

namespace Ankand.Data.Services
{
    public interface IOrdersServices
    {
        Task StoreOrderAsync(List<ShopinCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole);
    }
}
