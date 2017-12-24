using System.Net.Http;

namespace Service.Common.Abstract
{
    public interface IHttpClientService
    {
        HttpClient Client { get; }
    }
}
