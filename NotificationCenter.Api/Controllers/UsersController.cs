using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationCenter.Application.User.Commands;
using NotificationCenter.Api.Common;
using NotificationCenter.Application.User.DTOs;

namespace NotificationCenter.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<CreatedUserDto>> AddUser([FromBody] AddUserCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult(this);
    }
}