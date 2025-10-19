using MediatR;
using NotificationCenter.Application.Abstractions;
using NotificationCenter.Application.Common;
using NotificationCenter.Application.User.Commands;
using NotificationCenter.Application.User.DTOs;
using NotificationCenter.Application.User.Repository;

namespace NotificationCenter.Application.User.Handlers;

public class AddUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<AddUserCommand, Result<CreatedUserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CreatedUserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var doesExists = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);
        if (doesExists)
            return Result<CreatedUserDto>.BadRequest("A user with the provided email already exists.");

        var user = new Domain.Entities.User
        {
            Name = request.Name,
            LastName = request.LastName,
            Email = request.Email
        };
        
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var createdUserDto = new CreatedUserDto
        {
            Guid = user.Id,
        };

        return Result<CreatedUserDto>.Created(createdUserDto);
    }
}