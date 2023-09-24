using OnTime.Application.Pagination;

namespace OnTime.Application.Restaurants.Queries;

public class GetAllRestaurantsQuery : IRequest<ApplicationResponse<PaginationInfo<RestaurantResponseDto>>>
{
    public GetAllRestaurantsQuery(int pageSize, int pageNumber)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
    }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}
