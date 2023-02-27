using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET: /movies
        public ActionResult Index()
        {
            /*
            List<Movie> movies = _context.Movies
                .Include(m => m.Genre)
                .ToList();

            return View(movies);
            */
            return View();
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            Movie mv = _context.Movies
              .Include(c => c.Genre)
              .SingleOrDefault(c => c.Id == id);

            if (mv == null)
            {
                return HttpNotFound();
            }

            return View(mv);
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = _context.Movies
               .Include(m => m.Genre)
               .First();

            List<Customer> customers = _context.Customers
                .Include(c => c.MembershipType)
                .ToList();

            RandomMovieViewModel viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            //ViewData["movie"] = movie;
            //ViewBag.Movie = movie;

            return View(viewModel);
        }

        //GET: /movies/edit/1 OR /movies/edit?id=1
        public ActionResult Edit(int id)
        {
            Movie mv = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (mv == null)
            {
                return HttpNotFound();
            }

            List<Genre> genres = _context.Genres.ToList();

            MovieFormViewModel vm = new MovieFormViewModel(mv)
            {
                Genres = genres,
            };

            return View("MovieForm", vm);
        }

        //GET: /movies/new
        public ActionResult New()
        {
            List<Genre> genres = _context.Genres.ToList();

            MovieFormViewModel vm = new MovieFormViewModel()
            {
                Genres = genres,
                // Movie = new Movie()
            };

            return View("MovieForm", vm);
        }

        //POST: /movies/new
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel vm = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList(),
                };

                return View("MovieForm", vm);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                Movie dbMovie = _context.Movies.Single(m => m.Id == movie.Id);

                dbMovie.Name = movie.Name;
                dbMovie.ReleaseDate = movie.ReleaseDate;
                dbMovie.GenreId = movie.GenreId;
                dbMovie.StockNumber = movie.StockNumber;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}


/*
 * 
 *         List<Movie> list = new List<Movie>()
           {
                new Movie{Id=1, Name="Shrek"},
                new Movie{Id=2,Name="Wall-e"}
           };

            MovieViewModel movies = new MovieViewModel()
            {
                Movies = list
            };
 * 
       public ActionResult Index(int? pageIndex, string sortBy)
       {
           if (!pageIndex.HasValue)
               pageIndex = 1;

           if (string.IsNullOrWhiteSpace(sortBy))
               sortBy = "Name";

           return Content($"pageIndex={pageIndex}&sortBy={sortBy}");

       }
       */