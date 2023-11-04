using LTA.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LTA.WebAPI.Controllers;

[ApiExceptionFilter]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}