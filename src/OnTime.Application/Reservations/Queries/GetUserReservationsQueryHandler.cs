using System.Security.Claims;
using AutoMapper;
using OnTime.Application.Pagination;

namespace OnTime.Application.Reservations.Queries;

public class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, ApplicationResponse<PaginationInfo<ReservationResponseDto>>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;
    private readonly IPaginationRepository<ReservationResponseDto> _paginationRepository;

    public GetUserReservationsQueryHandler(IReservationRepository reservationRepository, IPaginationRepository<ReservationResponseDto> paginationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _paginationRepository = paginationRepository ??
    throw new ArgumentNullException(nameof(paginationRepository));
    }

    public async Task<ApplicationResponse<PaginationInfo<ReservationResponseDto>>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
    {
        var userIdClaimValue = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdClaimValue, out var userId))
        {
            var userReservations = _reservationRepository.GetUserReservations(userId).Select(r => _mapper.Map<ReservationResponseDto>(r)).AsQueryable();

            return await new Pagination<ReservationResponseDto>(_paginationRepository).CreateAsync(userReservations, request.PageNumber, request.PageSize);
        }
        else
        {
            return ApplicationResponse.BadRequest<PaginationInfo<ReservationResponseDto>>("Wrong User Id.");
        }
    }
}
