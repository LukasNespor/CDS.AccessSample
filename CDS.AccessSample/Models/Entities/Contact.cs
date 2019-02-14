using Newtonsoft.Json;

namespace CDS.AccessSample.Models.Entities
{
    public class Contact
    {
        [JsonProperty("fullname")]
        public string Name { get; set; }

        [JsonProperty("emailaddress1")]
        public string Email { get; set; }
    }
}
