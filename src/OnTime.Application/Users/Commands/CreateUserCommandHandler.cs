using System.Globalization;
using AutoMapper;

namespace OnTime.Application.Users.Commands;

public class CreateUserCommandHandler :
    IRequestHandler<CreateUserCommand, ApplicationResponse<AuthenticationResponseDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICreateTokenService _createTokenService;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, ICreateTokenService createTokenService)
    {
        _userRepository = userRepository ??
            throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createTokenService = createTokenService ?? throw new ArgumentNullException(nameof(_createTokenService));
    }

    public async Task<ApplicationResponse<AuthenticationResponseDto>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var validationResult = new UserValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
            return ApplicationResponse.BadRequest<AuthenticationResponseDto>(errorMessages);
        }

        var user = _mapper.Map<User>(request);
        var userExists = await _userRepository.Any(request.Email);

        if (userExists)
        {
            return ApplicationResponse.BadRequest<AuthenticationResponseDto>("Email address is already taken.");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        user.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.FirstName.ToLower());
        user.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(request.LastName.ToLower());
        user.Email = request.Email.ToLower();
        var newUser = await _userRepository.Add(user);

        return ApplicationResponse.Success(new AuthenticationResponseDto(_createTokenService.CreateToken(newUser)));
    }
}
