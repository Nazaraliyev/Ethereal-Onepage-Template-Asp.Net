﻿using System.ComponentModel.DataAnnotations;

namespace Ethereal_Onepage_Template_Asp.Net.Models
{
    public class SosialMedia
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(50), Required]
        public string Name { get; set; }


        [MaxLength(50), Required]
        public string Icon { get; set; }


        [MaxLength(300), Required]
        public string Link { get; set; }

    }

}
