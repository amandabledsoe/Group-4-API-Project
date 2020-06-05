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

        public MovieController(IConfiguration configuration)
        {
            _movieDAL = new MovieDAL(configuration);
        }
        public async Task<IActionResult> IndexAsync()
        {
            Movie movie = await  _movieDAL.GetMovie(76341);
            return View("Index", movie);
        }
    }
}
