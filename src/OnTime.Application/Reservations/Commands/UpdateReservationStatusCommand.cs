using OnTime.Domain.Enums;

namespace OnTime.Application.Reservations.Commands;

public class UpdateReservationStatusCommand : IRequest<ApplicationResponse<ReservationResponseDto>>
{
    public int Id { get; set; }

    public ReservationStatus Status { get; set; }
}
