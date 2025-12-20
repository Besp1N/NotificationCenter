using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NotificationCenter.Api.Controllers;
using NotificationCenter.Application.Common;
using NotificationCenter.Application.User.DTOs;
using NotificationCenter.Application.User.Queries;

namespace NotificationCenter.Tests.Controller;

using Xunit;

public class UserControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UsersController _controller;

    public UserControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UsersController(_mediatorMock.Object);
    }
    
    [Fact]
    public async Task GetUsers_ReturnsOkWithUserList_WhenUsersExist()
    {
        var users = new List<UserDto>
        {
            new UserDto { Guid = Guid.NewGuid(), Email = "test1@example.com", Name = "Test User 1", LastName = "test"},
            new UserDto { Guid = Guid.NewGuid(), Email = "test2@example.com", Name = "Test User 2", LastName = "test"},
        };

        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<IEnumerable<UserDto>>.Success(users));
        
        var result = await _controller.GetAllUsers();
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
        Assert.Equal(users, returnedUsers);
    }
}