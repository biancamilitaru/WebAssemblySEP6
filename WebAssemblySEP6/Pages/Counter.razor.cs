using Model;
using WebAssemblySEP6.Communication;

namespace WebAssemblySEP6.Pages;

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
        _userCommunication.AddUserAsync(new User(){Name = "dsh", Password = "pas", EmailAddress = "Email"});
    }
}

// TODO - delete this file before deploy