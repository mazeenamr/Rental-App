using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using vidly.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using vidly.ViewModel;


namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _Context;

        public MoviesController() 
        {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CAN_MANAGE_MOVIES))
                return View("List");
            return View("ReadOnlyList");

        }

        public ActionResult Details(int id)
        {
            var movie = _Context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.id == id);

            if (movie == null)
                return HttpNotFound();
            return View(movie);
          

        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Save(Movie Movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel()
                {
                    Movie = Movie,
                    Genre = _Context.Genres.ToList()
                };
                return View("newMovie", viewModel);


            }
            if (Movie.id == 0)
                _Context.Movies.Add(Movie);
            else
            {
                var MoviesInDb = _Context.Movies.SingleOrDefault(c => c.id == Movie.id);

                MoviesInDb.Name = Movie.Name;
                MoviesInDb.Genre = Movie.Genre;
                MoviesInDb.NumberInStock = Movie.NumberInStock;
                MoviesInDb.ReleaseDate = Movie.ReleaseDate;
            }

            _Context.SaveChanges();

           
            return RedirectToAction("Index", ("Movies"));
        }
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public ActionResult newMovie()
        {
            var Genre = _Context.Genres;
            var ViewModel = new NewMovieViewModel
            {
                Movie = new Movie(),
                Genre = Genre
            };
            return View("newMovie", ViewModel);
        }
        public ActionResult Edit(int id)
        {
            var movie = _Context.Movies.SingleOrDefault(c => c.id == id);
            var ViewModel = new NewMovieViewModel
            {
                Movie = movie,
                Genre= _Context.Genres.ToList()
            };
            return View("newMovie", ViewModel);
        }
    }
}