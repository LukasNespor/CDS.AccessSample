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
        /// Connect to Common Data Service for Apps and load Account entities including currency and contacts.
        /// </summary>
        /// <param name="args">No arguments necessary</param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            if (!IsConfigAndArgumentsValid(args))
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
                    EntityCollection = "accounts",
                    Select = new List<string>()
                    {
                        "accountid",
                        "name",
                        "description"
                    },
                    Filter = new List<string>()
                    {
                        "name eq 'KPCS'"
                    },
                    Expand = new List<string>()
                    {
                        "transactioncurrencyid($select=currencyname,currencysymbol,isocurrencycode)",
                        "crcef_AccountContacts($select=fullname,emailaddress1)" //custom relationship to Contact entity
                    },
                    OrderBy = "name desc"
                };

                var data = await client.GetEntitiesAsync<Account>(query);
            }
        }

        static bool IsConfigAndArgumentsValid(string[] args)
        {
            return args.Length > 0 && Config.IsValid;
        }
    }
}
