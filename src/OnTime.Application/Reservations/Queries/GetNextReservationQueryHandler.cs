using System.Security.Claims;
using AutoMapper;

namespace OnTime.Application.Reservations.Queries;

public class GetNextReservationQueryHandler : IRequestHandler<GetNextReservationQuery, ApplicationResponse<ReservationResponseDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetNextReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ??
    throw new ArgumentNullException(nameof(reservationRepository));

        _mapper = mapper ??
    throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReservationResponseDto>> Handle(GetNextReservationQuery request, CancellationToken cancellationToken)
    {
        var userIdClaimValue = request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdClaimValue, out var userId))
        {
            var nextReservation = await _reservationRepository.GetNext(userId);

            return ApplicationResponse.Success(_mapper.Map<ReservationResponseDto>(nextReservation));
        }
        else
        {
            return ApplicationResponse.BadRequest<ReservationResponseDto>("Wrong User Id.");
        }
    }
}
