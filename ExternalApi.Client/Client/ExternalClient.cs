using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalApi.Client.Abstract;
using ExternalApi.Client.Config;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Service.Common.Abstract;
using Service.Common.Entities.Models;

namespace ExternalApi.Client.Client
{
    public class ExternalClient : IExternalClient
    {
        private readonly ExternalClientConfig _config;
        private readonly IHttpClientService _http;

        private const string UsersPrefix = "/users";
        private const string AlbumsPrefix = "/albums";

        public ExternalClient(IOptions<ExternalClientConfig> config, IHttpClientService http)
        {
            _config = config.Value;
            _http = http;
        }

        public async Task<IEnumerable<User>> GetAllUsers(string availableForEmail)
        {
            var url = _config.Url + UsersPrefix;
            var response = await ProcessRequest<List<User>>(url);

            foreach (var user in response)
            {
                if (user.Email != availableForEmail)
                {
                    user.Email = string.Empty;
                }
            }

            return response;
        }

        public async Task<User> GetUser(int id, string availableForEmail)
        {
            var url = _config.Url + UsersPrefix + "/" + id;
            var user = await ProcessRequest<User>(url);

            if (user.Email != availableForEmail)
            {
                user.Email = string.Empty;
            }

            return user;
        }

        public async Task<IEnumerable<Album>> GetAllAlbums()
        {
            var url = _config.Url + AlbumsPrefix;
            return await ProcessRequest<List<Album>>(url);
        }

        public async Task<Album> GetAlbum(int id)
        {
            var url = _config.Url + AlbumsPrefix + "/" + id;
            return await ProcessRequest<Album>(url);
        }

        public async Task<IEnumerable<Album>> GetAlbumsByUser(int userId)
        {
            var url = _config.Url + AlbumsPrefix + "?userId=" + userId;
            return await ProcessRequest<List<Album>>(url);
        }

        private async Task<T> ProcessRequest<T>(string url)
        {
            var response = await _http.Client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

    }
}
