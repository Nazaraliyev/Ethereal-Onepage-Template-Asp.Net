using System.ComponentModel.DataAnnotations;

namespace Ethereal_Onepage_Template_Asp.Net.ViewModel
{
    public class VmLogin
    {
        [MaxLength(100), Required, EmailAddress]
        public string Email { get; set; }



        [Required]
        public string Password { get; set; }

    }
}
