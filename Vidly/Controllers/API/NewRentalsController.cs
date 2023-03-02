using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class NewRentalsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //POST: /api/newrentals
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDTO newRentalDTO)
        {
            Customer cst = _context.Customers
                                   .Single(c => c.Id == newRentalDTO.CustomerId);

            List<Movie> movies = _context.Movies
                                         .Where(m => newRentalDTO.MovieIds.Contains(m.Id))
                                         .ToList();

            foreach (Movie movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available");

                movie.NumberAvailable--;

                var rental = new Rental()
                {
                    Customer = cst,
                    Movie = movie,
                    DateRented = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok();
        }
    }
}

/*
 * Defensive approach conditional statements
 * if (cst == null)
    return BadRequest("Customer Id is not valid");
 * 
 * if (newRentalDTO.MovieIds.Count == 0)
       return BadRequest("No MovieIds have been given");

if (movies.Count != newRentalDTO.MovieIds.Count)
    return BadRequest("One or more movie Ids are invalid");
 */