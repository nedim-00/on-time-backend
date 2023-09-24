using OnTime.Domain;
using OnTime.Domain.Enums;

namespace OnTime.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly OnTimeDbContext _onTimeDbContext;

    public UserRepository(OnTimeDbContext onTimeDbContext)
    {
        _onTimeDbContext = onTimeDbContext;
    }

    public async Task<bool> Any(string email)
    {
        return await _onTimeDbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User> Add(User user)
    {
        var newUser = await _onTimeDbContext.Users.AddAsync(user);

        _ = await _onTimeDbContext.SaveChangesAsync();

        return newUser.Entity;
    }

    public async Task<User> UpdateUserRole(int id, UserRole userRole)
    {
        var user = await GetById(id);
        user.UserRole = userRole;

        _ = await _onTimeDbContext.SaveChangesAsync();

        return await GetById(id);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _onTimeDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetById(int id)
    {
        return await _onTimeDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> Update(User user, int id)
    {
        var updatedUser = await GetById(id);
        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.PhoneNumber = user.PhoneNumber;
        updatedUser.Image = user.Image;

        _ = await _onTimeDbContext.SaveChangesAsync();

        return updatedUser;
    }
}
