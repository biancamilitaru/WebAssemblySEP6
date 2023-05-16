using System.Threading.Tasks;
using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages
{

    public partial class Counter
    {
        private int currentCount = 0;
        private IUserCommunication _userCommunication;

        protected override async Task OnInitializedAsync()
        {
            _userCommunication = new UserCommunication();
        }

        private void IncrementCount()
        {
            _userCommunication.AddUserAsync(new User()
                {UserId = 2, Name = "name1", Password = "password1", EmailAddress = "email1"});
        }
    }
}

// TODO - delete this file before deploy