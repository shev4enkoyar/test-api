using LTA.Application.Albums.Queries;
using LTA.Application.Albums.Queries.GetAlbumById;
using LTA.Application.Albums.Queries.GetAlbumsByUserId;
using LTA.Application.Albums.Queries.GetAllAlbums;
using Microsoft.AspNetCore.Mvc;

namespace LTA.WebAPI.Controllers;

[Route("/api/albums")]
[ResponseCache(CacheProfileName = "Default5")]
public class AlbumController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AlbumDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<AlbumDto>>> GetAlbums(int? userId)
    {
        if (userId != null)
            return await Mediator.Send(new GetAlbumsByUserIdQuery(userId.Value));
        return await Mediator.Send(new GetAllAlbumsQuery());
    }

    [HttpGet("{albumId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlbumDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AlbumDto>> GetAlbumById(int albumId)
    {
        return await Mediator.Send(new GetAlbumByIdQuery(albumId));
    }
}