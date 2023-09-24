using AutoMapper;

namespace OnTime.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationResponse<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ??
          throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<UserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updatedRestaurant = await _userRepository.Update(_mapper.Map<User>(request), request.Id);

        return ApplicationResponse.Success(_mapper.Map<UserResponseDto>(updatedRestaurant));
    }
}
