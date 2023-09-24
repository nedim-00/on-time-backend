using AutoMapper;

namespace OnTime.Application.Restaurants.Commands;

public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, ApplicationResponse<RestaurantResponseDto>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository ??
          throw new ArgumentNullException(nameof(restaurantRepository));
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<RestaurantResponseDto>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var updatedRestaurant = await _restaurantRepository.Update(_mapper.Map<Restaurant>(request), request.Id);

        return ApplicationResponse.Success(_mapper.Map<RestaurantResponseDto>(updatedRestaurant));
    }
}
