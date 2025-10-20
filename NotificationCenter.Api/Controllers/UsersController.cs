using System.Collections;
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

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <param name="request">Request contains name, lastname and email of new user</param>
    /// <returns>Return Guid of new created user.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedUserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatedUserDto>> AddUser([FromBody] AddUserCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ToActionResult(this);
    }
    
    
    /// <summary>
    /// Return a list of all users in the system.
    /// </summary>
    /// <returns>Return a list of all users in the system.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
    {
        var result = await _mediator.Send(new Application.User.Queries.GetAllUsersCommand());
        return result.ToActionResult(this);
    }
}