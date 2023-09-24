using Ankand.Data;
using Ankand.Data.Cart;
using Ankand.Data.Services;
using Ankand.Data.ViewModels;
using Ankand.Data.Services;
using Ankand.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ankand.Data.Services;

namespace e_Book.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProduktService _produktService;
        private readonly ShopingCart _shopingCart;
        private readonly IOrdersServices _ordersServices;
        public OrdersController(IProduktService produktService, ShopingCart shopingCart, IOrdersServices ordersServices)
        {
            _produktService = produktService;
            _shopingCart = shopingCart;
            _ordersServices= ordersServices;
        }
       
        public IActionResult GetOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = _ordersServices.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);

        }
        public IActionResult Index()
        {
            
            var items=_shopingCart.GetShopinCartItems();
            _shopingCart.ShopinCartItems = items;
            var response = new ShopingCartVM()
            {
                ShopingCart = _shopingCart,
                ShopingCartTotal = _shopingCart.GetShoppingCartTotal()

            };
            return View(response);
        }
        public IActionResult AddItemToShoppingCart(int id)
        {
            var item = _produktService.GetOfertById(id);
            if (item != null)
            {
                _shopingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CompleteOrder()
        {
            var items = _shopingCart.GetShopinCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

             _ordersServices.StoreOrderAsync(items, userId, userEmailAddress);
             _shopingCart.clearShppinCartAsync();
            return View("OrderCompleter");
;        }
    }
}
