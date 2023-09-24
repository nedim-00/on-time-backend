using System.ComponentModel.DataAnnotations;
using OnTime.Domain.Enums;

namespace OnTime.Domain.Entities;

public class Reservation
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int NumberOfGuests { get; set; }

    [Required]
    public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Active;

    public string? SpecialComment { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan StartTime { get; set; }

    [Required]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public TimeSpan EndTime { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int RestaurantId { get; set; }

    [Required]
    public int TableId { get; set; }

    public User? User { get; set; }

    public Restaurant? Restaurant { get; set; }

    public Table? Tables { get; set; }
}
