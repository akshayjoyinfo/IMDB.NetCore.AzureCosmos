using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IMovieAPIAdapterService
    {
        Task<IEnumerable<MovieSearchResult>> GetMovieListsByQueryAsync(MovieSearchQuery searchQuery);
        Task<MovieViewModel> GetDomainMovieByTheMovieDbAPI(MovieQuery movieQuery);
    }
}
