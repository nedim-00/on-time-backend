using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Application.Reservations.Commands;

public class CreateReservationCommand : IRequest<ApplicationResponse<ReservationResponseDto>>
{
    public required int NumberOfGuests { get; set; }

    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Active;

    public string? SpecialComment { get; set; }

    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan StartTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public required TimeSpan EndTime { get; set; }

    public required int UserId { get; set; }

    public required int RestaurantId { get; set; }
}
