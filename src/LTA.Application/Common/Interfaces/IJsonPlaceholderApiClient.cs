using LTA.Domain.Entities.Json;

namespace LTA.Application.Common.Interfaces;

public interface IJsonPlaceholderApiClient
{
    Task<List<AlbumJson>?> GetAlbums();

    Task<List<AlbumJson>?> GetAlbumsByUserId(int userId);

    Task<List<UserJson>?> GetUsers();

    Task<AlbumJson?> GetAlbumById(int albumId);

    Task<UserJson?> GetUserById(int userId);
}