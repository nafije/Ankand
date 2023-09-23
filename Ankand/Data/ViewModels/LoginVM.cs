using System.ComponentModel.DataAnnotations;

namespace Ankand.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="Email address")]
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddres { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
