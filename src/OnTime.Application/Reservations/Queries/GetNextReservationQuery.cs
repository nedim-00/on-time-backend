using Microsoft.AspNetCore.Http;

namespace OnTime.Application.Reservations.Queries;

public class GetNextReservationQuery : IRequest<ApplicationResponse<ReservationResponseDto>>
{
    public HttpContext? HttpContext { get; set; }
}
