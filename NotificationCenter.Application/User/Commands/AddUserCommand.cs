using System.ComponentModel.DataAnnotations;
using MediatR;
using NotificationCenter.Application.Common;
using NotificationCenter.Application.User.DTOs;

namespace NotificationCenter.Application.User.Commands;

public sealed record AddUserCommand(
    string Name,
    string LastName,
    string Email
) : IRequest<Result<CreatedUserDto>>;

