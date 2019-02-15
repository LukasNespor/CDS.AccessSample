using Newtonsoft.Json;

namespace CDS.AccessSample.Models.Entities
{
    public class SalesOrderLine
    {
        [JsonProperty("crcef_salesorderlinestatus")]
        public string Status { get; set; }

        [JsonProperty("crcef_salesordernumber")]
        public string SalesOrderNumber { get; set; }
    }
}
