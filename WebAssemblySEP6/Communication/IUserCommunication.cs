using System.Threading.Tasks;
using Model;

namespace WebAssemblySEP6.Communication
{
    public interface IUserCommunication
    {
        public Task AddUserAsync(User userToAdd);
        public Task<bool> IsEmailAddressUsed(User user);
        public Task<User> LogIn(User user);
        public Task<User> GetUserById(int userId);
    }
}