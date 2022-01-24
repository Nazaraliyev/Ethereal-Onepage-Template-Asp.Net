using Ethereal_Onepage_Template_Asp.Net.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Ethereal_Onepage_Template_Asp.Net.ViewModel
{
    public class VmAccountIndex
    {
        public List<CustomUser>  customUsers { get; set; }
        public List<IdentityRole> roles { get; set; }
        public List<IdentityUserRole<string>> UserRole { get; set; }
    }
}
