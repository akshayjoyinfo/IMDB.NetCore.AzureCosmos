using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp.Models
{
    [CosmosCollection(Container ="TestMovies")]
    public class TestMovie
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
