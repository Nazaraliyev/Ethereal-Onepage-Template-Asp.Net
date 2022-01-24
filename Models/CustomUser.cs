using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal_Onepage_Template_Asp.Net.Models
{
    public class CustomUser:IdentityUser
    {
        [MaxLength(50), Required]
        public string Name { get; set; }


        [MaxLength(50), Required]
        public string Surname { get; set; }


        public string Profile { get; set; }


        [NotMapped]
        public IFormFile ProfileFile{ get; set; }

    }
}
