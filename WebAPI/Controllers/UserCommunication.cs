using System.Threading.Tasks;
using WebAssemblySEP6.Model;

namespace WebAPI.Controllers
{
    public class UserCommunication : IUserCommunication
    {
        public Task<User> RegisterUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> VerifyEmailAddressAsync(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}