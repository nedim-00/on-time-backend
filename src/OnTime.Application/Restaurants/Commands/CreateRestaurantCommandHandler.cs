using AutoMapper;

namespace OnTime.Application.Restaurants.Commands;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, ApplicationResponse<RestaurantResponseDto>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository ??
            throw new ArgumentNullException(nameof(restaurantRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<RestaurantResponseDto>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new RestaurantValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
            return ApplicationResponse.BadRequest<RestaurantResponseDto>(errorMessages);
        }

        var newRestaurant = await _restaurantRepository.Add(_mapper.Map<Restaurant>(request));

        return ApplicationResponse.Success(_mapper.Map<RestaurantResponseDto>(newRestaurant));
    }
}
