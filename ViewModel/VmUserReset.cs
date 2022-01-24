using System.ComponentModel.DataAnnotations;

namespace Ethereal_Onepage_Template_Asp.Net.ViewModel
{
    public class VmUserReset
    {
        [MaxLength(100), Required, MinLength(5)]
        public string Password { get; set; }


        [MaxLength(100), Required, MinLength(5), Compare("Password"), Display(Name = "Confirm Password")]
        public string CoPassword { get; set; }

        public string Id { get; set; }
    }
}
