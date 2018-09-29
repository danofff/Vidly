using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MoviesDto
    {
        [Required]
        public int Id { get; set; }
        [StringLength(30)]
        [Required()]
        public string Name { get; set; }

        [Required]
        public int? GenreId { get; set; }
        public Genre Genre { get; set; }
 
        [Required]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

    }
}