using Model;

namespace SEP6Backend.DataAccess;

public interface IUserDataAccess
{
    public Task AddUserAsync(User user);
}