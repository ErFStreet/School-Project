namespace Domain.ViewModels.User;

public class TokenUserViewModel : object
{
    public TokenUserViewModel() : base()
    {
    }

    public required string FullName { get; set; }

    public required string UserName { get; set; }

    public required string Role { get; set; }
}
