using MediatR;
using NotificationCenter.Application.Common;
using NotificationCenter.Application.User.DTOs;
using NotificationCenter.Application.User.Queries;
using NotificationCenter.Application.User.Repository;

namespace NotificationCenter.Application.User.Handlers;

public class GetAllUsersHandler(IUserRepository userRepository)
    : IRequestHandler<GetAllUsersCommand, Result<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);
        if (users.Count == 0)
            return Result<IEnumerable<UserDto>>.NotFound("No users found.");

        var usersList = users.Select(
            user => new UserDto
            {
                Guid = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();

        return Result<IEnumerable<UserDto>>.Success(usersList);
    }
}