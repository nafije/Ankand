using Ankand.Data;
using Ankand.Models;
using Microsoft.EntityFrameworkCore;

namespace Ankand.Data.Services
{
    public class OrdersService : IOrdersServices
    {

        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context=context;
        }
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId,string userRole)
        {

            var orders = await _context.Order.Include(n=>n.OrderItems).ThenInclude(n=>n.Oferta).Include(n=>n.UserId).ToListAsync();
            //if (userRole!="Admin")
            //{
                orders=orders.Where(n=>n.UserId==userId).ToList();
            //}
            return orders;
        }

        public async Task StoreOrderAsync(List<ShopinCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Order.AddAsync(order);  
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProduktID = item.Produkti.ID,
                    OrderId = order.Id,
                    Price = item.Produkti.OfertaPrice

                };
                await _context.OrderItem.AddAsync(orderItem);
                await _context.SaveChangesAsync();
            }

        }
    }
}
