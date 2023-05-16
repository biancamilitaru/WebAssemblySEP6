using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace WebAPI.Controllers
{
    public interface IUserCommunication
    {
        Task<User> RegisterUserAsync(User user);
        Task<bool> VerifyEmailAddressAsync(User user);
    }
}