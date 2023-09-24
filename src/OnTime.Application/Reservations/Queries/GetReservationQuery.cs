namespace OnTime.Application.Reservations.Queries;

public class GetReservationQuery : IRequest<ApplicationResponse<ReservationResponseDto>>
{
    public int Id { get; set; }
}
