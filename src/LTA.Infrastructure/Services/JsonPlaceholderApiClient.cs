using System.Net.Http.Json;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities.Json;

namespace LTA.Infrastructure.Services;

public class JsonPlaceholderApiClient : IJsonPlaceholderApiClient
{
    public const string ClientName = "jsonplaceholderapi";

    private readonly IHttpClientFactory _httpClientFactory;

    public JsonPlaceholderApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<AlbumJson>?> GetAlbums()
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync("albums");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<AlbumJson>>();
    }

    public async Task<List<AlbumJson>?> GetAlbumsByUserId(int userId)
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync($"albums?userId={userId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<AlbumJson>>();
    }

    public async Task<List<UserJson>?> GetUsers()
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync("users");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<List<UserJson>>();
    }

    public async Task<AlbumJson?> GetAlbumById(int albumId)
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync($"albums/{albumId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<AlbumJson>();
    }

    public async Task<UserJson?> GetUserById(int userId)
    {
        var client = _httpClientFactory.CreateClient(ClientName);

        var response = await client.GetAsync($"users/{userId}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<UserJson>();
    }
}