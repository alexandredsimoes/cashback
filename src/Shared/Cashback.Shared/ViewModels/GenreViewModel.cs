using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Shared
{
    public class GenreViewModel
    {
        [JsonIgnore()]
        public int Id { get; set; }
        [JsonProperty("id")]
        public string Identifier { get; set; }
        public string HRef { get; set; }
        public string Name { get; set; }
        public GenreIconViewModel[] Icons { get; set; }
    }

    public class GenreIconViewModel
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Url { get; set; }
    }
}
