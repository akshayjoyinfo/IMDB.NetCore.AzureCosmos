using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class APIPersonResponse
    {
        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("deathday")]
        public object Deathday { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("also_known_as")]
        public List<string> AlsoKnownAs { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }
    }
}
