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
                    EntityCollection = "crcef_salesorders",
                    Select = new List<string>()
                    {
                        "crcef_salesorderid",
                        "crcef_salesordername",
                        "crcef_salesordernumber",
                        "crcef_ordercreationdatetime"
                    },
                    Filter = new List<string>()
                    {
                        "crcef_salesordername eq '3 Fazy'"
                    },
                    Expand = new List<string>()
                    {
                        "crcef_SalesOrderLines($select=crcef_salesorderlinestatus,crcef_salesordernumber)"
                    },
                    OrderBy = "crcef_salesordernumber desc"
                };

                var data = await client.GetEntitiesAsync<SalesOrder>(query);
            }
        }
    }
}
