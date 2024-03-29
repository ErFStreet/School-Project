namespace Domain.ViewModels.User;

public class DetailUserViewModel : object
{
    public DetailUserViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public string? Password { get; set; }

    public int? ClassId { get; set; }

    public string? ClassCode { get; set; }
}
