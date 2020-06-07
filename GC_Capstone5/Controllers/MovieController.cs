using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GC_Capstone5.Models;
using Microsoft.AspNetCore.Authorization;
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


        public async Task<IActionResult> GetMoviesByTitle(string keyword, string pageNumber)
        {
            SearchRootobject searchResults = await _movieDAL.GetMovieByKeyword(keyword,pageNumber); //replace with method name from DAL that returns results by title
            ViewBag.keyword = keyword;
            return View("SearchResults", searchResults);
        }
        [Authorize]
        public IActionResult Favorites()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value; //gets user ID
            List<Favorite> favorites = new List<Favorite>();
            favorites = _context.Favorite.Where(x => x.UserId == id).ToList();
            return View(favorites);
        }
        [Authorize]
        public async Task<IActionResult> AddToFavoritesAsync(int id)
        {
            Movie movieToAdd = await _movieDAL.GetMovie(id);
            Favorite newFav = new Favorite();

            newFav.Title = movieToAdd.title;
            newFav.ReleaseDate = movieToAdd.release_date;
            newFav.RunTime = movieToAdd.runtime;
            newFav.PosterPath = movieToAdd.poster_path;
            newFav.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (ModelState.IsValid)
            {
                _context.Favorite.Add(newFav); 
                _context.SaveChanges();
            }

            return RedirectToAction("Favorites");
        }
        [Authorize]

        public IActionResult RemoveFromFavorites(int id)
        {
            Favorite toRemove = _context.Favorite.Find(id);
            if (toRemove != null)
            {
                _context.Favorite.Remove(toRemove);
                _context.SaveChanges();
            }
            return RedirectToAction("Favorites");
        }





        public async Task<IActionResult> IndexAsync()
        {
            Movie movie = await _movieDAL.GetMovie(76341);
            return View("Index", movie);
        }

    }
}
