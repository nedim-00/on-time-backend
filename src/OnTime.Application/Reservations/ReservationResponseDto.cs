using System.ComponentModel.DataAnnotations;
using OnTime.Application.Restaurants;
using OnTime.Application.Users;
using OnTime.Domain.Enums;

namespace OnTime.Application.Reservations;

public record ReservationResponseDto
{
    public int Id { get; set; }

    public int NumberOfGuests { get; set; }

    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Active;

    public string? SpecialComment { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan StartTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan EndTime { get; set; }

    public int UserId { get; set; }

    public int TableId { get; set; }

    public BasicRestaurantInformationResponseDto? Restaurant { get; set; }

    public BasicUserInformationResposneDto? User { get; set; }
}
