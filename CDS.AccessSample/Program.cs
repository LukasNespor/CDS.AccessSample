using CDS.AccessSample.Code;
using CDS.AccessSample.Models;
using CDS.AccessSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CDS.AccessSample
{
    class Program
    {
        /// <summary>
        /// Connect to Common Data Service for Apps and load Sales Order entities including Sales Order Lines.
        /// </summary>
        /// <param name="args">No arguments necessary</param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            if (!Config.IsValid)
            {
                Console.WriteLine("Missing configuration values in app settings.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            using (ClientContext client = new ClientContext(Config.EnvironmentName))
            {
                await client.ConnectAsync(Config.AzureAppId, Config.UserName, Config.Password);

                var query = new Query
                {
                    EntityCollection = "cds_salesorders",
                    Select = new List<string>()
                    {
                        "cds_salesorderid",
                        "cds_salesordername",
                        "cds_salesordernumber",
                        "cds_ordercreationdatetime",
                        "cds_invoiceaddresscity"
                    },
                    Filter = new List<string>()
                    {
                        "cds_salesordername eq '3 Fazy'"
                    },
                    Expand = new List<string>()
                    {
                        "cds_salesorderlines($select=cds_lineamount,cds_linedescription,cds_confirmedshippingdate)"
                    },
                    OrderBy = "cds_salesordernumber desc"
                };

                var data = await client.GetEntitiesAsync<SalesOrder>(query);
            }
        }
    }
}
