using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ethereal_Onepage_Template_Asp.Net.ViewModel
{
    public class VmUserRegister
    {
        [MaxLength(50), Required, MinLength(5)]
        public string Name { get; set; }



        [MaxLength(50), Required, MinLength(5)]
        public string Surname { get; set; }



        [MaxLength(50), Required, EmailAddress]
        public string Email { get; set; }



        [MaxLength(15), Required, Phone]
        public string Phone { get; set; }


        public IFormFile ProfileFile { get; set; }



        public List<IdentityRole> role { get; set; }


        [Required]
        public string RoleId { get; set; }
    }
}
