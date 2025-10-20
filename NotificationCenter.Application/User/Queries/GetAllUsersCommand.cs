using MediatR;
using NotificationCenter.Application.Common;
using NotificationCenter.Application.User.DTOs;

namespace NotificationCenter.Application.User.Queries;

public record GetAllUsersCommand() : IRequest<Result<IEnumerable<UserDto>>>;