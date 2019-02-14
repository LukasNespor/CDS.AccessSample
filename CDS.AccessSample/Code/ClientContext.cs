using CDS.AccessSample.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CDS.AccessSample.Code
{
    public class ClientContext : IDisposable
    {
        static HttpClient client;
        static Uri baseAddress;
        static readonly string pageSizeHeaderName = "Prefer";

        public ClientContext(string environmentName, string apiVersion = "v9.1")
        {
            baseAddress = new Uri($"https://{environmentName}.api.crm4.dynamics.com/api/data/{apiVersion}");
        }

        /// <summary>
        /// Acquires access token and set to authentication header.
        /// </summary>
        /// <param name="clientId">Azure application ID</param>
        /// <param name="userName">User name</param>
        /// <param name="password">Password for provided user name</param>
        /// <returns>Task</returns>
        public async Task ConnectAsync(string clientId, string userName, string password)
        {
            var authResult = await GetTokenAsync(clientId, userName, password);

            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            client.DefaultRequestHeaders.Add("OData-MaxVersion", "4.0");
            client.DefaultRequestHeaders.Add("OData-Version", "4.0");
        }

        /// <summary>
        /// Get access token to CDS
        /// </summary>
        /// <param name="clientId">Azure application ID</param>
        /// <param name="userName">User name</param>
        /// <param name="password">Password for provided user name</param>
        /// <returns>AuthenticationResult</returns>
        async Task<AuthenticationResult> GetTokenAsync(string clientId, string userName, string password)
        {
            var authParameters = await AuthenticationParameters.CreateFromResourceUrlAsync(baseAddress);
            var authContext = new AuthenticationContext(authParameters.Authority, false);
            return await authContext.AcquireTokenAsync(
                $"{baseAddress.Scheme}://{baseAddress.Authority}",
                clientId,
                new UserCredential(userName, password));
        }

        /// <summary>
        /// Get entities by query helper class using pagination.
        /// </summary>
        /// <typeparam name="T">Type of entity to serialize</typeparam>
        /// <param name="query">Query definition</param>
        /// <returns>List of entities</returns>
        public async Task<IEnumerable<T>> GetEntitiesAsync<T>(Query query)
        {
            if (query.PageSize > 0)
            {
                var headers = client.DefaultRequestHeaders;
                if (headers.Contains(pageSizeHeaderName))
                    headers.Remove(pageSizeHeaderName);

                headers.Add(pageSizeHeaderName, $"odata.maxpagesize={query.PageSize}");
            }

            var entities = new List<T>();
            string url = $"{baseAddress}/{query.Url}";

            while (true)
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<ODataResponse<T>>(data);
                    entities.AddRange(obj.Value);

                    if (!string.IsNullOrEmpty(obj.NextLink))
                    {
                        url = obj.NextLink;
                        continue;
                    }

                    return entities;
                }
            }
        }

        public void Dispose()
        {
            if (client != null)
                client.Dispose();
        }
    }
}
