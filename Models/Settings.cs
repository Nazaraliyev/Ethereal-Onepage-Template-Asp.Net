using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal_Onepage_Template_Asp.Net.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }



        [MaxLength(250)]
        public string Logo { get; set; }



        [MaxLength(100)]
        public string Header { get; set; }



        [MaxLength(300)]
        public string SubHeader { get; set; }


        [MaxLength(250)]
        public string HeaderBackImage { get; set; }


        [NotMapped]
        public IFormFile HeaderBackImageFile { get; set; }



        [MaxLength(300)]
        public string Subscribe { get; set; }


        [MaxLength(250)]
        public string SubscribeBackImage { get; set; }


        [NotMapped]
        public IFormFile SubscribeBackImageFile { get; set; }


        [MaxLength(2000)]
        public string About { get; set; }



        [MaxLength(300)]
        public string Address { get; set; }



        [MaxLength(100)]
        public string Email { get; set; }



        [MaxLength(15)]
        public string Phone { get; set; }
    }
}
