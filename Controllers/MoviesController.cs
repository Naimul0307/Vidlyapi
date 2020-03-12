using System;
using VidlyApi.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using VidlyApi.Models;

namespace VidlyApi.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext dbContext;

        public MoviesController()
        {
            dbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }
        public ActionResult NewMovie()
        {
            var Genres = dbContext.Genres.ToList();
            var ViewModel = new NewMovieViewModel
            {
                Genres = Genres
            };
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                dbContext.Movies.Add(movie);
            }
            else
            {
                var movieInDb = dbContext.Movies.SingleOrDefault(m => m.Id == movie.Id);
                movieInDb.Name = movieInDb.Name;
                movieInDb.GenreId = movieInDb.GenreId;
                movieInDb.ReleaseDate = movieInDb.ReleaseDate;
                movieInDb.DateAdded = movieInDb.DateAdded;
                movieInDb.NumberInStock = movieInDb.NumberInStock;
            }
            dbContext.SaveChanges();
            return RedirectToAction("Index","Movies");
        }
        // GET: Movies
        public ViewResult Index()
        {
            var movie = dbContext.Movies.Include(m=>m.Genre).ToList();
            return View(movie);
        }
        //public ActionResult Details(int id)
        //{
        //    var movie = dbContext.Movies.SingleOrDefault(m => m.Id == id);
        //    if (movie == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(movie);
        //}
        public ActionResult Edit(int id)
        {
            var movie = dbContext.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var ViewModel = new NewMovieViewModel()
            {
                Movie = movie,
                Genres = dbContext.Genres.ToList()
            };
            return View("NewMovie" , ViewModel);
        }
    }
}