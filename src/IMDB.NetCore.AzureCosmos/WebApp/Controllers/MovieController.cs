using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieAPIAdapterService _movieAPIAdapterService;
        private readonly IMovieDomainService _movieDomainService;
        
        public MovieController(ILogger<MovieController> logger, IMovieAPIAdapterService movieAPIAdapterService, IMovieDomainService movieDomainService,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _movieAPIAdapterService = movieAPIAdapterService;
            _movieDomainService = movieDomainService;
        }
        [ActionName("Index")]
        public async Task<ActionResult<List<Movie>>> Index()
        {
            return View(await _movieDomainService.GetAllMovies());
        }

        [ActionName("Create")]
        public async Task<ActionResult> Create(int id)
        {
            if(id==0)
                return View(new MovieViewModel());
            else
            {
                var movieImported = await _movieAPIAdapterService.GetDomainMovieByTheMovieDbAPI(new MovieQuery() { id = id });
                return View(movieImported);
            }    
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel item)
        {
            await Task.CompletedTask;
            var movieAdded = await _movieDomainService.AddAsync(item);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            Task.Run(async () =>
            {
                _movieDomainService.AddCredits(item, movieAdded);
            });
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            return RedirectToAction("Index");
        }


        // https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core
    }
}