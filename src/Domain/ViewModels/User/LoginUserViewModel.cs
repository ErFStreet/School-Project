namespace Domain.ViewModels.User;

public class LoginUserViewModel : object
{
    public LoginUserViewModel() : base()
    {
    }

    public required string UserName { get; set; }

    public required string Password { get; set; }
}
