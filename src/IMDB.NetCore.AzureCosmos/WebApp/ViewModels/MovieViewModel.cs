using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
            this.MovieDetails = new Movie();
            this.CastDetails = new List<Cast>();
            this.CrewDetails = new List<Crew>();
        }
        public Movie MovieDetails { get; set; }

        public List<Cast> CastDetails { get; set; }

        public List<Crew> CrewDetails { get; set; }
    }
}
