using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [StringLength(30)]
        [Required()]
        public string Name { get; set; }
       
        public Genre Genre { get; set; }

        [Required]
        public int? GenreId { get; set; }

        [Required]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Display(Name="Number in stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }

        [Required]
        [Display(Name="Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
    }
}