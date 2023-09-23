using Ankand.Models;
using System.ComponentModel.DataAnnotations;
namespace Ankand.Data.ViewModels
{
    public class PostViewModel
    {
        public IEnumerable<Produkti> Products { get; set; }
        public List<bool> IsCurrentUserCreatorList { get; set; }
    }

}
