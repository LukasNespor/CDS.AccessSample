using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CDS.AccessSample.Models.Entities
{
    public class SalesOrder
    {
        [JsonProperty("crcef_salesorderid")]
        public string Id { get; set; }

        [JsonProperty("crcef_salesordername")]
        public string Name { get; set; }

        [JsonProperty("crcef_salesordernumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("crcef_ordercreationdatetime")]
        public DateTimeOffset OrderCreationDateTime { get; set; }

        [JsonProperty("crcef_SalesOrderLines")]
        public List<SalesOrderLine> SalesOrderLines { get; set; }
    }
}
