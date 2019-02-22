using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CDS.AccessSample.Models.Entities
{
    public class SalesOrder
    {
        [JsonProperty("cds_salesorderid")]
        public string Id { get; set; }

        [JsonProperty("cds_salesordername")]
        public string Name { get; set; }

        [JsonProperty("cds_salesordernumber")]
        public string OrderNumber { get; set; }

        [JsonProperty("cds_ordercreationdatetime")]
        public DateTimeOffset OrderCreationDateTime { get; set; }

        [JsonProperty("cds_invoiceaddresscity")]
        public string InvoiceAddressCity { get; set; }

        [JsonProperty("cds_salesorderlines")]
        public List<SalesOrderLine> SalesOrderLines { get; set; }
    }
}
