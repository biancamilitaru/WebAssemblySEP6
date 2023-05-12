using System.Dynamic;
using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace WebAssemblySEP6.Communication
{
    public interface IUserCommunication
    {
        Task<User> RegisterUserAsync(User user);
        Task<bool> VerifyEmailAddressAsync(User user);
    }
}