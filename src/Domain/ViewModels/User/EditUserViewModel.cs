namespace Domain.ViewModels.User;

public class EditUserViewModel : object
{
    public EditUserViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string UserName { get; set; }

    public int? ClassId { get; set; }
}
