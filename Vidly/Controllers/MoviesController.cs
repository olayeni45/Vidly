using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //GET: /movies
        public ActionResult Index()
        {
            List<Movie> list = new List<Movie>()
           {
                new Movie{Id=1, Name="Shrek"},
                new Movie{Id=2,Name="Wall-e"}
           };

            MovieViewModel movies = new MovieViewModel()
            {
                Movies = list
            };

            return View(movies);
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek" };

            List<Customer> customers = new List<Customer>()
            {
                new Customer{ Name="Customer 1"},
                new Customer {Name="Customer 2"}
            };

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
            return Content("Id = " + id);
        }





        //GET: /movies/release/{year}/{month}
        //Route attribute: [Route()]
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}


/*
       public ActionResult Index(int? pageIndex, string sortBy)
       {
           if (!pageIndex.HasValue)
               pageIndex = 1;

           if (string.IsNullOrWhiteSpace(sortBy))
               sortBy = "Name";

           return Content($"pageIndex={pageIndex}&sortBy={sortBy}");

       }
       */