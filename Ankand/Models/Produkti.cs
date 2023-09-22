using Ankand.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ankand.Models
{
    public class Produkti
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Image")]
        public string ImageURL { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int BiderId { get; set; }
        [ForeignKey("BiderId")]
        public ApplicationUser ApplicationUser { get; set; }
        public List<Oferta> Oferta { get; set; }
    }
}
