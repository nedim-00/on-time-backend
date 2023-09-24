using AutoMapper;

namespace OnTime.Application.Reservations.Queries;

public class GetReservationQueryHandler : IRequestHandler<GetReservationQuery, ApplicationResponse<ReservationResponseDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));

        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReservationResponseDto>> Handle(GetReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetById(request.Id);

        return ApplicationResponse.Success(_mapper.Map<ReservationResponseDto>(reservation));
    }
}
