using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context= new ApplicationDbContext();
        }

        //movies
        [Route("Movies")]
        public ActionResult Index()
        {

            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int? id=null)
        {
            var sendMovie = _context.Movies.Include(c => c.Genre).SingleOrDefault(m=>m.Id == id);

            if (sendMovie == null)
            {
                return HttpNotFound();
            }
            return View(sendMovie);
        }

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie(){Name="Shrek!"};

            var customers = new List<Customer>
            {
                new Customer() {Name = "Customer 1"},
                new Customer() {Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
           
        }

        public ActionResult New()
        {           
            var genres = _context.Genre.ToList();
            MovieForViewModel movieViewModel = new MovieForViewModel()
            {
                Genre = genres
            };
            return View("MovieForm",movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieViewModel=new MovieForViewModel()
                {
                    Movie = movie,
                    Genre = _context.Genre.ToList()
                };
                return View("MovieForm", movieViewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded=DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(s => s.Id == movie.Id);
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Name = movie.Name;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

           
            _context.SaveChanges();
        
            return RedirectToAction("Index","Movies");
        }

        public ActionResult Edit(int? id)
        {            
            var editMovie = _context.Movies.SingleOrDefault(s => s.Id == id);
            if (editMovie == null)
            {
                return HttpNotFound();
            }
            else
            {
                MovieForViewModel movieViewModel=new MovieForViewModel()
                {
                    Genre = _context.Genre,
                    Movie = editMovie
                };
                return View("MovieForm", movieViewModel);
            }        
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+ "/"+ month);
        }
         
    }
}