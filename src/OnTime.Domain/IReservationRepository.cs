using OnTime.Domain.Enums;

namespace OnTime.Domain;

public interface IReservationRepository
{
    Task<Reservation> Add(Reservation reservation);

    Task<Reservation> GetById(int id);

    Task<Reservation> GetNext(int id);

    IQueryable<Reservation> GetAllReservations();

    IQueryable<Reservation> GetRestaurantReservations(int id, DateTime date);

    IQueryable<Reservation> GetUserReservations(int userId);

    IQueryable<Reservation> GetRestaurantReservations(int restaurantId);

    Task<Reservation> UpdateReservationStatus(int id, ReservationStatus status);
}
