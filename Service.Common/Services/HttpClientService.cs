using System.Net.Http;
using Service.Common.Abstract;

namespace Service.Common.Services
{
    public class HttpClientService : IHttpClientService
    {
        private static HttpClient _client = new HttpClient();
        public HttpClient Client
        {
            get
            {
                return _client;
            }
        }
    }
}
