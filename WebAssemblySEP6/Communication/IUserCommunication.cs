using Model;

namespace WebAssemblySEP6.Communication;

public interface IUserCommunication
{
    public Task AddUserAsync(User userToAdd);
}