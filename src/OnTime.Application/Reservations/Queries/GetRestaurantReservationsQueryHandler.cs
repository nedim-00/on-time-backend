using AutoMapper;
using OnTime.Application.Pagination;

namespace OnTime.Application.Reservations.Queries;

public class GetRestaurantReservationsQueryHandler : IRequestHandler<GetRestaurantReservationsQuery, ApplicationResponse<PaginationInfo<ReservationResponseDto>>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly IPaginationRepository<ReservationResponseDto> _paginationRepository;

    public GetRestaurantReservationsQueryHandler(IReservationRepository reservationRepository, IPaginationRepository<ReservationResponseDto> paginationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _paginationRepository = paginationRepository ??
    throw new ArgumentNullException(nameof(paginationRepository));
    }

    public async Task<ApplicationResponse<PaginationInfo<ReservationResponseDto>>> Handle(GetRestaurantReservationsQuery request, CancellationToken cancellationToken)
    {
        var restaurantReservations = _reservationRepository.GetRestaurantReservations(request.Id).Select(r => _mapper.Map<ReservationResponseDto>(r)).AsQueryable();

        return await new Pagination<ReservationResponseDto>(_paginationRepository).CreateAsync(restaurantReservations, request.PageNumber, request.PageSize);

        // }
        // else
        // {
        //    return ApplicationResponse.BadRequest<PaginationInfo<ReservationResponseDto>>("Wrong User Id.");
        // }
    }
}
