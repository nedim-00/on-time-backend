using System.Collections.ObjectModel;
using System.Security.Claims;
using AutoMapper;

namespace OnTime.Application.Restaurants.Queries;

public class GetOwnedRestaurantsQueryHandler : IRequestHandler<GetOwnedRestaurantsQuery, ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public GetOwnedRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>> Handle(GetOwnedRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var userIdClaimValue = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdClaimValue, out var userId))
        {
            var ownedRestaurants = await _restaurantRepository.GetOwnedRestaurants(userId);

            return ApplicationResponse.Success(ownedRestaurants.Select(_mapper.Map<RestaurantResponseDto>).ToList().AsReadOnly());
        }
        else
        {
            return ApplicationResponse.BadRequest<ReadOnlyCollection<RestaurantResponseDto>>("Bad User Id provided.");
        }
    }
}
