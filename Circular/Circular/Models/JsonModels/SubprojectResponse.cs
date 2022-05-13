using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circular.Models.JsonModels
{
    public class SubprojectResponse
    {
        public string name { get; set; }
        //public Owner owner { get; set; }

        [JsonProperty("html_url")]
        public string url { get; set; }

        public string language { get; set; }

        public DateTime created_at { get; set; }

        public int ProjectId { get; set; }

        
    }

    //public class Owner
    //{
    //    [JsonProperty("login")]
    //    public string name { get; set; }
    //}
}
