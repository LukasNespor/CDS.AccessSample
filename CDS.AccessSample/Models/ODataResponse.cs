using Newtonsoft.Json;
using System.Collections.Generic;

namespace CDS.AccessSample.Models
{
    class ODataResponse<T>
    {
        [JsonProperty("@odata.nextLink")]
        public string NextLink { get; set; }
        public List<T> Value { get; set; }
    }
}
