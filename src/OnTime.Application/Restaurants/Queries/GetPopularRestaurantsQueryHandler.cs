using System.Collections.ObjectModel;
using AutoMapper;

namespace OnTime.Application.Restaurants.Queries;

public class GetPopularRestaurantsQueryHandler : IRequestHandler<GetPopularRestaurantsQuery, ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetPopularRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository ??
            throw new ArgumentNullException(nameof(restaurantRepository));

        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>> Handle(GetPopularRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var popularRestaurants = await _restaurantRepository.GetPopularRestaurants();

        return ApplicationResponse.Success(popularRestaurants.Select(_mapper.Map<RestaurantResponseDto>).ToList().AsReadOnly());
    }
}
