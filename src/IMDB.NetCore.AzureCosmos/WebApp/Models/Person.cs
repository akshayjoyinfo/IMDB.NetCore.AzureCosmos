using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Helpers;

namespace WebApp.Models
{
    [CosmosCollection(Container = "Persons")]
    public class Person
    {
        [JsonProperty("birthday")]
        public DateTime? Birthday { get; set; }

        [JsonProperty("deathday")]
        public object Deathday { get; set; }

        [JsonProperty("id")]
        [Key]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }
    }
}
