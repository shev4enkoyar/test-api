using LTA.Application.Users.Queries;
using LTA.Application.Users.Queries.GetAllUsers;
using LTA.Application.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Mvc;

namespace LTA.WebAPI.Controllers;

[Route("/api/users")]
[ResponseCache(CacheProfileName = "Default")]
public class UserController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        return await Mediator.Send(new GetAllUsersQuery());
    }

    [HttpGet("{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserDto>> GetUserById(int userId)
    {
        return await Mediator.Send(new GetUserByIdQuery(userId));
    }
}