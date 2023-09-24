using OnTime.Domain;
using OnTime.Domain.Enums;

namespace OnTime.Infrastructure;

public class ReservationRepository : IReservationRepository
{
    private readonly OnTimeDbContext _onTimeDbContext;

    public ReservationRepository(OnTimeDbContext onTimeDbContext)
    {
        _onTimeDbContext = onTimeDbContext;
    }

    public async Task<Reservation> Add(Reservation reservation)
    {
        var newRestaurant = await _onTimeDbContext.Reservations.AddAsync(reservation);

        _ = await _onTimeDbContext.SaveChangesAsync();

        return newRestaurant.Entity;
    }

    public IQueryable<Reservation> GetAllReservations()
    {
        return _onTimeDbContext.Reservations.OrderByDescending(r => r.Date).ThenByDescending(r => r.StartTime).AsQueryable();
    }

    public async Task<Reservation> GetById(int id)
    {
        return await _onTimeDbContext.Reservations.Include(r => r.Restaurant).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Reservation> GetNext(int id)
    {
        return await _onTimeDbContext.Reservations.OrderBy(r => r.Date).ThenBy(r => r.StartTime).Where(r => r.UserId == id & r.ReservationStatus == ReservationStatus.Active).FirstOrDefaultAsync();
    }

    public IQueryable<Reservation> GetRestaurantReservations(int id, DateTime date)
    {
        return _onTimeDbContext.Reservations.Include(r => r.Restaurant).Where(r => r.RestaurantId == id && r.Date == date).AsQueryable();
    }

    public IQueryable<Reservation> GetUserReservations(int userId)
    {
        return _onTimeDbContext.Reservations.Include(r => r.Restaurant).Where(r => r.UserId == userId).OrderByDescending(r => r.Date).ThenByDescending(r => r.StartTime).AsQueryable();
    }

    public IQueryable<Reservation> GetRestaurantReservations(int restaurantId)
    {
        return _onTimeDbContext.Reservations.Include(r => r.User).Where(r => r.RestaurantId == restaurantId).OrderByDescending(r => r.Date).ThenByDescending(r => r.StartTime).AsQueryable();
    }

    public async Task<Reservation> UpdateReservationStatus(int id, ReservationStatus status)
    {
        var reservation = await GetById(id);
        reservation.ReservationStatus = status;

        _ = await _onTimeDbContext.SaveChangesAsync();

        return reservation;
    }
}
