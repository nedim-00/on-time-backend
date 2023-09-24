using AutoMapper;

namespace OnTime.Application.Users.Queries;

public class GetUserInformationQueryHandler : IRequestHandler<GetUserInformationQuery, ApplicationResponse<UserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUserInformationQueryHandler(IUserRepository userRepository, IMapper mapper, IUserService userService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper;
    }

    public async Task<ApplicationResponse<UserResponseDto>> Handle(GetUserInformationQuery request, CancellationToken cancellationToken)
    {
        var user = _userService.Email();
        var result = _mapper.Map<UserResponseDto>(await _userRepository.GetByEmail(user));
        return ApplicationResponse.Success(result);
    }
}
