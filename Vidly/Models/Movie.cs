using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int GenreId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in Stock")]
        [Required]
        [Range(1, 20, ErrorMessage = "The field Number must be between 1 and 20.")]
        public int StockNumber { get; set; }
    }
}