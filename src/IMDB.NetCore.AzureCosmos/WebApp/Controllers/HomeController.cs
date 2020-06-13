using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieAPIAdapterService _movieAdapterService;
        

        public HomeController(ILogger<HomeController> logger, IMovieAPIAdapterService movieAdapterService)
        {
            _logger = logger;
            _movieAdapterService = movieAdapterService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ActionName("Search")]
        public IActionResult Search()
        {
            return View(new MovieSearch() { query = string.Empty, Results = new List<MovieSearchResult>() });
        }

        [HttpPost]
        [ActionName("Search")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind("query")] MovieSearch item)
        {
            if (ModelState.IsValid)
            {
                MovieSearchQuery query = new MovieSearchQuery() { query = item.query };
                var movieResults = await _movieAdapterService.GetMovieListsByQueryAsync(query);
                return View(new MovieSearch() { query= item.query, Results = movieResults });
            }

            return View(item);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
