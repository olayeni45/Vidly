using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "The field Number must be between 1 and 20.")]
        public int StockNumber { get; set; }
    }
}