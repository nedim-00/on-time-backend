using OnTime.Domain;

namespace OnTime.Infrastructure;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly OnTimeDbContext _onTimeDbContext;

    public RestaurantRepository(OnTimeDbContext onTimeDbContext)
    {
        _onTimeDbContext = onTimeDbContext;
    }

    public async Task<Restaurant> Add(Restaurant restaurant)
    {
        var newRestaurant = await _onTimeDbContext.Restaurants.AddAsync(restaurant);
        _ = await _onTimeDbContext.SaveChangesAsync();

        return newRestaurant.Entity;
    }

    public IQueryable<Restaurant> GetAllRestaurants()
    {
        return _onTimeDbContext.Restaurants.Include(r => r.Tables).Include(r => r.Menus).ThenInclude(m => m.MenuItems).AsQueryable();
    }

    public IQueryable<Restaurant> SearchRestaurants(string name)
    {
        return _onTimeDbContext.Restaurants.Include(r => r.Tables).Include(r => r.Menus).ThenInclude(m => m.MenuItems).Where(r => r.Name.Contains(name)).AsQueryable();
    }

    public async Task<Restaurant> GetById(int id)
    {
        return await _onTimeDbContext.Restaurants.Include(r => r.Tables).Include(r => r.Menus).ThenInclude(m => m.MenuItems).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyCollection<Restaurant>> GetOwnedRestaurants(int userId)
    {
        return await _onTimeDbContext.Restaurants.Include(r => r.Tables).Include(r => r.Menus).ThenInclude(m => m.MenuItems).Where(r => r.UserId == userId).ToListAsync();
    }

    public async Task<IReadOnlyCollection<Restaurant>> GetPopularRestaurants()
    {
        return await (from restaurant in _onTimeDbContext.Restaurants
                      join reservation in _onTimeDbContext.Reservations
                      on restaurant.Id equals reservation.RestaurantId
                      group restaurant by new
                      {
                          restaurant.Id,
                          restaurant.Name,
                          restaurant.Description,
                          restaurant.OpenTime,
                          restaurant.CloseTime,
                          restaurant.Image,
                          restaurant.City,
                          restaurant.Municipality,
                          restaurant.Address,
                          restaurant.UserId,
                      }
                        into grouped
                      orderby grouped.Count() descending
                      select new Restaurant
                      {
                          Id = grouped.Key.Id,
                          Name = grouped.Key.Name,
                          Description = grouped.Key.Description,
                          OpenTime = grouped.Key.OpenTime,
                          CloseTime = grouped.Key.CloseTime,
                          Image = grouped.Key.Image,
                          City = grouped.Key.City,
                          Municipality = grouped.Key.Municipality,
                          Address = grouped.Key.Address,
                          UserId = grouped.Key.UserId,
                      }).Take(5).ToListAsync();
    }

    public async Task<Restaurant> Update(Restaurant restaurant, int id)
    {
        var updatedRestaurant = await GetById(id);
        updatedRestaurant.Name = restaurant.Name;
        updatedRestaurant.Address = restaurant.Address;
        updatedRestaurant.City = restaurant.City;
        updatedRestaurant.Municipality = restaurant.Municipality;
        updatedRestaurant.PhoneNumber = restaurant.PhoneNumber;
        updatedRestaurant.Description = restaurant.Description;
        updatedRestaurant.Image = restaurant.Image;
        updatedRestaurant.OpenTime = restaurant.OpenTime;
        updatedRestaurant.CloseTime = restaurant.CloseTime;
        updatedRestaurant.UserId = restaurant.UserId;

        _ = await _onTimeDbContext.SaveChangesAsync();

        return updatedRestaurant;
    }
}
