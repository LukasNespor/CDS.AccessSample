using Newtonsoft.Json;
using System.Collections.Generic;

namespace CDS.AccessSample.Models.Entities
{
    public class Account
    {
        [JsonProperty("accountid")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("transactioncurrencyid")]
        public Currency Currency { get; set; }

        [JsonProperty("crcef_AccountContacts")]
        public List<Contact> Contacts { get; set; }
    }
}
