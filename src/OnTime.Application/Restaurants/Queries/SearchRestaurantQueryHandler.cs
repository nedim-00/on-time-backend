using AutoMapper;
using OnTime.Application.Pagination;

namespace OnTime.Application.Restaurants.Queries;

public class SearchRestaurantQueryHandler : IRequestHandler<SearchRestaurantQuery,
    ApplicationResponse<PaginationInfo<RestaurantResponseDto>>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IPaginationRepository<RestaurantResponseDto> _paginationRepository;
    private readonly IMapper _mapper;

    public SearchRestaurantQueryHandler(
        IMapper mapper,
        IRestaurantRepository restaurantRepository,
        IPaginationRepository<RestaurantResponseDto> paginationRepository)
    {
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper));
        _restaurantRepository = restaurantRepository ??
            throw new ArgumentNullException(nameof(restaurantRepository));
        _paginationRepository = paginationRepository ??
            throw new ArgumentNullException(nameof(paginationRepository));
    }

    public async Task<ApplicationResponse<PaginationInfo<RestaurantResponseDto>>> Handle(SearchRestaurantQuery request, CancellationToken cancellationToken)
    {
        var restaurantsQuery = _restaurantRepository.SearchRestaurants(request.Name).Select(r => _mapper.Map<RestaurantResponseDto>(r)).AsQueryable();

        return await new Pagination<RestaurantResponseDto>(_paginationRepository).CreateAsync(restaurantsQuery, request.PageNumber, request.PageSize);
    }
}
