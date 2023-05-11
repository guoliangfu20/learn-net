using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Clients
{
    public class NamedOrderServiceClient
    {
        IHttpClientFactory _httpClientFactory;

        string _clientName = "NamedOrderServiceClient";


        public NamedOrderServiceClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {
            var client = _httpClientFactory.CreateClient(_clientName);

            return await client.GetStringAsync("OrderService");
        }
    }
}
