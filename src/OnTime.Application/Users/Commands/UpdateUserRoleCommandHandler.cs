using AutoMapper;

namespace OnTime.Application.Users.Commands;

public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, ApplicationResponse<UpdateUserResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICreateTokenService _createTokenService;

    public UpdateUserRoleCommandHandler(IUserRepository userRepository, IMapper mapper, ICreateTokenService createTokenService)
    {
        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));

        _createTokenService = createTokenService ?? throw new ArgumentNullException(nameof(_createTokenService));
    }

    public async Task<ApplicationResponse<UpdateUserResponseDto>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new UpdateUserValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
            return ApplicationResponse.BadRequest<UpdateUserResponseDto>(errorMessages);
        }

        var updatedUser = await _userRepository.UpdateUserRole(request.UserId, request.UserRole);
        var newUserToken = new AuthenticationResponseDto(_createTokenService.CreateToken(updatedUser)).Token;
        var updatedUserResponse = _mapper.Map<UpdateUserResponseDto>(updatedUser);

        updatedUserResponse.Token = newUserToken;

        return ApplicationResponse.Success(updatedUserResponse);
    }
}
