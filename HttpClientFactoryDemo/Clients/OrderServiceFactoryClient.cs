using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.Clients
{
    /// <summary>
    /// 工厂模式
    /// </summary>
    public class OrderServiceFactoryClient
    {
        IHttpClientFactory _httpClientFactory;

        public OrderServiceFactoryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> Get()
        {
            var client = _httpClientFactory.CreateClient();

            //使用client发起HTTP请求
            return await client.GetStringAsync("https://localhost:5003/OrderService");
        }

    }
}
