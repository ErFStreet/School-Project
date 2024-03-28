namespace Domain.ViewModels.User;

public class ListUserViewModel:object
{
    public ListUserViewModel():base()
    {
    }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string RoleName { get; set; }

    public string? ClassCode { get; set; } = null;

}
