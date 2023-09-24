using AutoMapper;

namespace OnTime.Application.Restaurants.Queries;

public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, ApplicationResponse<RestaurantResponseDto>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetRestaurantQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository ??
            throw new ArgumentNullException(nameof(restaurantRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<RestaurantResponseDto>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetById(request.Id);

        return ApplicationResponse.Success(_mapper.Map<RestaurantResponseDto>(restaurant));
    }
}
