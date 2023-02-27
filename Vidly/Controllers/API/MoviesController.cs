using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/movies
        public IEnumerable<MovieDTO> GetMovies()
        {
            return _context.Movies
                .ToList()
                .Select(Mapper.Map<Movie, MovieDTO>);
        }

        //GET /api/movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDTO>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Movie movie = Mapper.Map<MovieDTO, Movie>(movieDTO);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDTO.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDTO);
        }

        //PUT /api/movies/{id}
        [HttpPut]
        public void UpdateMovie(int id, MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Movie dbMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (dbMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDTO, dbMovie);

            _context.SaveChanges();
        }

        //DELETE /api/customers/{id}
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            Movie dbMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (dbMovie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();
        }
    }
}
