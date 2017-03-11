using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ocelot.Cluster
{
    public class AvailableOcelotNodesGetter : IAvailableOcelotNodesGetter
    {
        private IKnownNodeUriProvider _provider;
        private HttpClient _httpClient;

        public AvailableOcelotNodesGetter(IKnownNodeUriProvider provider, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _provider = provider;
        }

        public async Task<List<Uri>> Get()
        {
            var knownClusterNode = _provider.Get();
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{knownClusterNode}/adminpath/knownnodes");
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", "client id and password");
            var response = await _httpClient.SendAsync(httpRequestMessage);
            var knownNodes = JsonConvert.DeserializeObject<List<Uri>>(await response.Content.ReadAsStringAsync());
            return knownNodes;
        }
    }

    public interface IAvailableOcelotNodesGetter
    {
        Task<List<Uri>> Get();
    }
}