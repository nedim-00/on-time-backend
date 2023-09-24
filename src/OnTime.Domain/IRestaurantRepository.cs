namespace OnTime.Domain;

public interface IRestaurantRepository
{
    Task<Restaurant> Add(Restaurant restaurant);

    Task<Restaurant> GetById(int id);

    Task<IReadOnlyCollection<Restaurant>> GetOwnedRestaurants(int userId);

    Task<IReadOnlyCollection<Restaurant>> GetPopularRestaurants();

    IQueryable<Restaurant> GetAllRestaurants();

    IQueryable<Restaurant> SearchRestaurants(string name);

    Task<Restaurant> Update(Restaurant restaurant, int id);
}
