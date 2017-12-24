using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalApi.Client.Abstract;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Entities.Models;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IExternalClient _client;

        public UsersController(IExternalClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            HttpContext.Request.Headers.TryGetValue("From", out var userEmail);

            return await _client.GetAllUsers(userEmail);
        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            HttpContext.Request.Headers.TryGetValue("From", out var userEmail);

            return await _client.GetUser(id, userEmail);
        }

        [HttpGet("{id}/albums")]
        public async Task<IEnumerable<Album>> GetAlbums(int id)
        {
            return await _client.GetAlbumsByUser(id);
        }

    }
}
