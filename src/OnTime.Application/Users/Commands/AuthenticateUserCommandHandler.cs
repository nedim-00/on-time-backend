namespace OnTime.Application.Users.Commands;

public class AuthenticateUserCommandHandler :
    IRequestHandler<AuthenticateUserCommand, ApplicationResponse<AuthenticationResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICreateTokenService _createTokenService;

    public AuthenticateUserCommandHandler(IUserRepository userRepository, ICreateTokenService createTokenService)
    {
        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));
        _createTokenService = createTokenService ?? throw new ArgumentNullException(nameof(_createTokenService));
    }

    public async Task<ApplicationResponse<AuthenticationResponseDto>> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        return user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)
            ? ApplicationResponse.BadRequest<AuthenticationResponseDto>("Invalid email or password.")
            : ApplicationResponse.Success(new AuthenticationResponseDto(_createTokenService.CreateToken(user)));
    }
}
