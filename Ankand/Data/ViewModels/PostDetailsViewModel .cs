using Ankand.Models;
using System.ComponentModel.DataAnnotations;
namespace Ankand.Data.ViewModels
{
    public class PostDetailsViewModel
    {
        public Produkti Produkti { get; set; }
        public bool IsCurrentUserCreator { get; set; }
    }

}
