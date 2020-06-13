using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp.Models
{
    [CosmosCollection(Container = "Casts")]
    public class Cast
    {   
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("person_id")]
        public long TheMovideDBPersonId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("movie_id")]
        public string MovieId { get; set; }
    }

    [CosmosCollection(Container = "Crews")]
    public class Crew
    {
        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("person_id")]
        public long TheMovideDBPersonId { get; set; }

        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public object ProfilePath { get; set; }

        [JsonProperty("movie_id")]
        public string MovieId { get; set; }
    }
}
