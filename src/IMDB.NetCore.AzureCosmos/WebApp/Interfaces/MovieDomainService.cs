using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IMovieDomainService
    {
        Task<Movie> AddAsync(MovieViewModel viewModel);

        Task<List<Movie>> GetAllMovies();

        Task AddCrews(MovieViewModel model, Movie movie);
        Task AddCasts(MovieViewModel model, Movie movie);

        Task AddCredits(MovieViewModel model, Movie movie);

        Task<Person> AddPerson(long theMoviePersonId);
    }
}
