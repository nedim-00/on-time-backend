using OnTime.Application.Pagination;

namespace OnTime.Application.Restaurants.Queries;

public class SearchRestaurantQuery : IRequest<ApplicationResponse<PaginationInfo<RestaurantResponseDto>>>
{
    public SearchRestaurantQuery(int pageSize, int pageNumber, string name)
    {
        PageSize = pageSize;
        PageNumber = pageNumber;
        Name = name;
    }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public string Name { get; set; }
}
