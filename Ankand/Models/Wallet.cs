using Ankand.Models;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ankand.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public int OfertId { get; set; }//id e ofertes se userit
        public string BidderId { get; set; }//useri qe ben ofert
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
     
    }
}
