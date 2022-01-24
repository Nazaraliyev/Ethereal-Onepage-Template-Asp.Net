using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ethereal_Onepage_Template_Asp.Net.Models
{
    public class Work
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(250)]
        public string WorkPhoto { get; set; }


        [NotMapped]
        public IFormFile WorkFile { get; set; }
    }

}
