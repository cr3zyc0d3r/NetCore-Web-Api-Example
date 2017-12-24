using System.Collections.Generic;
using System.Threading.Tasks;
using ExternalApi.Client.Abstract;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Entities.Models;

namespace ApiService.Controllers
{
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IExternalClient _client;

        public AlbumsController(IExternalClient client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IEnumerable<Album>> Get()
        {
            return await _client.GetAllAlbums();
        }

        [HttpGet("{id}")]
        public async Task<Album> Get(int id)
        {
            return await _client.GetAlbum(id);
        }

    }
}
