using Microsoft.AspNetCore.Http;
using OnTime.Application.Pagination;

namespace OnTime.Application.Reservations.Queries;

public class GetUserReservationsQuery : IRequest<ApplicationResponse<PaginationInfo<ReservationResponseDto>>>
{
    public int PageSize { get; set; }

    public int PageNumber { get; set; }

    public HttpContext? HttpContext { get; set; }

    public Restaurant? Restaurant { get; set; }
}
