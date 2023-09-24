using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;

namespace OnTime.Application.Restaurants.Queries;

public class GetOwnedRestaurantsQuery : IRequest<ApplicationResponse<ReadOnlyCollection<RestaurantResponseDto>>>
{
    public HttpContext? HttpContext { get; set; }
}
