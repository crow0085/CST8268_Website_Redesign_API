using System.Text.Json.Serialization;

namespace WebsiteRedesignAdminAPI.Models
{
    public class Person
    {
        public string name { get; set; }
        public string image { get; set; }
        public string team { get; set; }
        public string description { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int id { get; set; }

    }

    public class Image
    {
        public string width { get; set; }
        public string height { get; set; }
        public string path { get; set; }
    }
}
