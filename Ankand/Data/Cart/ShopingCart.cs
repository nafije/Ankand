using Ankand.Controllers;
using Ankand.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using static System.Reflection.Metadata.BlobBuilder;
using BooksModel = Ankand.Models.Oferta;
namespace Ankand.Data.Cart
{
    public class ShopingCart
    {
        public AppDbContext _context { get; set; }
        public string ShopinCartID { get; set; }
        public List<ShopinCartItem> ShopinCartItems { get; set; }
        public ShopingCart(AppDbContext context)
        {
            _context = context;
        }
        public static ShopingCart GetShopingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShopingCart(context) { ShopinCartID = cartId };
        }
        

        public void AddItemToCart(BooksModel produkti)
        {
            var shoppingCartItem = _context.ShopinCartItem.FirstOrDefault(n => n.Produkti.ID== produkti.ID && n.ShopingCartId == ShopinCartID);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShopinCartItem()
                {
                    ShopingCartId = ShopinCartID,
                    Produkti = produkti,
                    Amount = 1
                };

                _context.ShopinCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(BooksModel books)
        {
            var shoppingCartItem = _context.ShopinCartItem.FirstOrDefault(n => n.Produkti.ID == books.ID && n.ShopingCartId == ShopinCartID);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShopinCartItem.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }
        public List< ShopinCartItem>GetShopinCartItems()

        {
            return ShopinCartItems ?? (ShopinCartItems = _context.ShopinCartItem.Where(n => n.ShopingCartId == 
            ShopinCartID).Include(n => n.Produkti).ToList());
        }  
        public double GetShoppingCartTotal()=> _context.ShopinCartItem.Where(n => n.ShopingCartId == ShopinCartID).Select(n => n.Produkti.OfertaPrice ).Sum();
        
        public async Task clearShppinCartAsync()
        {
            var items =await _context.ShopinCartItem.Where(n => n.ShopingCartId ==
            ShopinCartID).ToListAsync();
            _context.ShopinCartItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
