using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Circular.Models.JsonModels
{
    //for right data formating
    class UnixTimeToDatetimeConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);


        public override void WriteJson(JsonWriter writer, object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }
            return _epoch.AddSeconds(Convert.ToDouble(reader.Value)).ToLocalTime();
        }
    }

    public class Author
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("date")]
        //[JsonConverter(typeof(UnixTimeToDatetimeConverter))]
        public DateTime date { get; set; }
    }

    public class Commit
    {
        public Author author { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("comment_count")]
        public int comment_count { get; set; }

        [JsonProperty("url")]
        public string commit_url { get; set; }
    }

    public class CommitResponse
    {
        [JsonProperty("sha")]
        public string sha { get; set; }

        public Commit commit { get; set; }
    }
}
