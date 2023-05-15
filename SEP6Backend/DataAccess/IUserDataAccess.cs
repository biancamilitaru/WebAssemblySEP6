using Model;

namespace SEP6Backend.DataAccess;

public interface IUserDataAccess
{
    public Task<User> AddUserAsync(User user);
}