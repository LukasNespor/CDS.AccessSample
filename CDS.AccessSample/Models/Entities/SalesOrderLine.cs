using Newtonsoft.Json;
using System;

namespace CDS.AccessSample.Models.Entities
{
    public class SalesOrderLine
    {
        [JsonProperty("cds_lineamount")]
        public decimal LineAmount { get; set; }

        [JsonProperty("cds_linedescription")]
        public string LineDescription { get; set; }

        [JsonProperty("cds_linereservationstatus")]
        public string LineReservationStatus { get; set; }

        [JsonProperty("cds_shippingnumber")]
        public string ShippingNumber { get; set; }

        [JsonProperty("cds_confirmedshippingdate")]
        public DateTimeOffset? ConfirmedShippingDate { get; set; }
    }
}
