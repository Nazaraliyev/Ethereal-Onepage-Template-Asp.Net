using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal_Onepage_Template_Asp.Net.Models
{
    public class Testimonal
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(100), Required]
        public string Name { get; set; }


        [MaxLength(50)]
        public string Profile { get; set; }


        [NotMapped]
        public IFormFile ProfileFile { get; set; }


        [MaxLength(1000), Required]
        public string Content { get; set; }

    }

}
