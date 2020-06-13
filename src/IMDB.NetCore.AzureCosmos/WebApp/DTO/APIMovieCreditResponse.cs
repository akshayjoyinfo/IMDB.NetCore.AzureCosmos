using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.DTO
{
    public class APIMovieCreditResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cast")]
        public List<CastDTO> Casts { get; set; }

        [JsonProperty("crew")]
        public List<CrewDTO> Crews { get; set; }
    }

    public partial class CastDTO
    {
        [JsonProperty("cast_id")]
        public long CastId { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }

    public partial class CrewDTO
    {
        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}
