using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service.Common.Entities.Models;

namespace ExternalApi.Client.Abstract
{
    public interface IExternalClient
    {
        Task<IEnumerable<User>> GetAllUsers(string availableForEmail);
        Task<User> GetUser(int id, string availableForEmail);
        Task<IEnumerable<Album>> GetAllAlbums();
        Task<Album> GetAlbum(int id);
        Task<IEnumerable<Album>> GetAlbumsByUser(int userId);
    }
}
