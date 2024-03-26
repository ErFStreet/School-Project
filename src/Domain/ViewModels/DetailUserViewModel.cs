namespace Domain.ViewModels;

public class DetailUserViewModel : object
{
    public DetailUserViewModel() : base()
    {
    }

    public string? FullName { get; set; }

    public string? Password { get; set; }

    public string? RoleName { get; set; }
}
