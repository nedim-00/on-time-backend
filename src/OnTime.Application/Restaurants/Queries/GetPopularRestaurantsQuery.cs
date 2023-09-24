using System.Collections.ObjectModel;

namespace OnTime.Application.Restaurants.Queries;

public class GetPopularRestaurantsQuery : IRequest<ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>>
{
}
