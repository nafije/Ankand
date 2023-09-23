using Ankand.Models;
using System.ComponentModel.DataAnnotations;
using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ankand.Models
{
    public class Oferta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Price")]
        public double OfertaPrice { get; set; }

        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ProduktID { get; set; }
        [ForeignKey("ProduktID")]
        public Produkti Produkti { get; set; }

        public int BiderId { get; set; }
        //[ForeignKey("BiderId")]
        //public ApplicationUser ApplicationUser { get; set; }

    }
}
