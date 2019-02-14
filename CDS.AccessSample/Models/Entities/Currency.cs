using Newtonsoft.Json;

namespace CDS.AccessSample.Models.Entities
{
    public class Currency
    {
        [JsonProperty("currencyname")]
        public string Name { get; set; }

        [JsonProperty("currencysymbol")]
        public string Symbol { get; set; }

        [JsonProperty("isocurrencycode")]
        public string Code { get; set; }
    }
}
