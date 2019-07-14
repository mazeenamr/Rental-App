using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vidly.Models
{
    public class Movie
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Name ")]
        public string Name { get; set; }
        
        public Genre  Genre { get; set; }
        [Required]
        [Display(Name = "Genre ")]
        public int GenreId { get; set; }
        [Required]
        [Display(Name = "Release date ")]
        public string  ReleaseDate { get; set; }
        
        public string  DateAdded { get; set; }
        [Required]
        [Display(Name = "Number in stock ")]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

    }
}