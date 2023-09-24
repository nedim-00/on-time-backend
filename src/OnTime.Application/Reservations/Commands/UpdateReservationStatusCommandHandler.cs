using AutoMapper;

namespace OnTime.Application.Reservations.Commands;

public class UpdateReservationStatusCommandHandler : IRequestHandler<UpdateReservationStatusCommand, ApplicationResponse<ReservationResponseDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public UpdateReservationStatusCommandHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));

        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ApplicationResponse<ReservationResponseDto>> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
    {
        var updatedReservation = await _reservationRepository.UpdateReservationStatus(request.Id, request.Status);

        return ApplicationResponse.Success(_mapper.Map<ReservationResponseDto>(updatedReservation));
    }
}
