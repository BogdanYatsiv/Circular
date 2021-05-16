using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.JsonModels
{
    class ProjectResponse
    {
        public string name { get; set; }
        public Owner owner { get; set; }

        [JsonProperty("html_url")]
        public string url { get; set; }

        public string language { get; set; }
    }

    class Owner
    {
        [JsonProperty("login")]
        public string name { get; set; }
    }
}
