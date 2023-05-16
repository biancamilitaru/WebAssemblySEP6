using Model;

namespace SEP6Backend.DataAccess;

public interface IUserDataAccess
{
    public Task AddUserAsync(User user);
    public Task<IList<User>> GetAllUsersAsync();

    public Task<User> GetUserById(int userId);
}