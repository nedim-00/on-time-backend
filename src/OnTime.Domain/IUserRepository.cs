using OnTime.Domain.Enums;

namespace OnTime.Domain;

public interface IUserRepository
{
    Task<User> Add(User user);

    Task<User> GetById(int id);

    Task<User> GetByEmail(string email);

    Task<User> UpdateUserRole(int id, UserRole userRole);

    Task<bool> Any(string email);

    Task<User> Update(User user, int id);
}
