using OnTime.Application.Pagination;

namespace OnTime.Application.Reservations.Queries;

public class GetRestaurantReservationsQuery : IRequest<ApplicationResponse<PaginationInfo<ReservationResponseDto>>>
{
    public required int Id { get; set; }

    public int PageSize { get; set; }

    public int PageNumber { get; set; }
}
