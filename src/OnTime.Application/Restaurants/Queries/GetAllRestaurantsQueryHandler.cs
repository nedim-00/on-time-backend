using AutoMapper;
using OnTime.Application.Pagination;

namespace OnTime.Application.Restaurants.Queries;

public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery,
    ApplicationResponse<PaginationInfo<RestaurantResponseDto>>>
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IPaginationRepository<RestaurantResponseDto> _paginationRepository;
    private readonly IMapper _mapper;

    public GetAllRestaurantsQueryHandler(
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

    public async Task<ApplicationResponse<PaginationInfo<RestaurantResponseDto>>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurantsQuery = _restaurantRepository.GetAllRestaurants().Select(r => _mapper.Map<RestaurantResponseDto>(r)).AsQueryable();

        return await new Pagination<RestaurantResponseDto>(_paginationRepository).CreateAsync(restaurantsQuery, request.PageNumber, request.PageSize);
    }
}
