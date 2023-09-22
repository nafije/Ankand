using System.ComponentModel.DataAnnotations;

namespace Ankand.Models
{
    public class ShopinCartItem
    {
        [Key]
        public int Id { get; set; }
        public Produkti Produkti { get; set; }
        public int Amount { get; set; }
        public string ShopingCartId { get; set; }
    }
}
