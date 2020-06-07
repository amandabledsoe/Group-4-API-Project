using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GC_Capstone5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GC_Capstone5.Controllers
{
    public class MovieController : Controller
    {
        //private readonly string _apiKey;
        private readonly MovieDAL _movieDAL;
        private readonly FavoriteMovieDbContext _context; 

        public MovieController(IConfiguration configuration, FavoriteMovieDbContext context) 
        {
            _movieDAL = new MovieDAL(configuration);
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> GetMoviesByTitle(string keyword)
        {
            SearchRootobject searchResults = await _movieDAL.GetMovieByKeyword(keyword,null); //replace with method name from DAL that returns results by title
            return View("SearchResults", searchResults);
        }

        //public IActionResult AddToFavorites(Movie movieToAdd)
        //{
        //    Favorite newFav = new Favorite();
        //    newFav.title = movieToAdd.title;
        //    newFav.genre = movieToAdd.genres; // not sure if this will be the same
        //    newFav.release_date = movieToAdd.release_date;
        //    newFav.runtime = movieToAdd.runtime;
        //    newFav.poster_path = movieToAdd.poster_path;
        //    //newFav.userID = code for grabbing current user ID - to be added later

        //    if (ModelState.IsValid)
        //    {
        //        _context.Favorite.Add(newFav);  //requires db to be setup
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("Favorites");
        //}


        //public IActionResult RemoveFromFavorites(int id)
        //{
        //    Favorite toRemove = _context.Favorite.Find(id);
        //    if(toRemove != null)
        //    {
        //        _context.Favorite.Remove(toRemove);
        //        _context.SaveChanges();
        //    }
        //    return RedirectToAction("Favorites");
        //}





        public async Task<IActionResult> IndexAsync()
        {
            Movie movie = await _movieDAL.GetMovie(76341);
            return View("Index", movie);
        }

    }
}
