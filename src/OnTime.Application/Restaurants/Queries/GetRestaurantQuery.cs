namespace OnTime.Application.Restaurants.Queries;

public class GetRestaurantQuery : IRequest<ApplicationResponse<RestaurantResponseDto>>
{
    public int Id { get; set; }
}
