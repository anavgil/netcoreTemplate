using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace netcoreTemplate.Infrastructure.Agents
{
    public class TestAgent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestAgent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task TestHttpClient()
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            await httpClient.GetAsync("");


            await Task.CompletedTask;
        }
    }
}
