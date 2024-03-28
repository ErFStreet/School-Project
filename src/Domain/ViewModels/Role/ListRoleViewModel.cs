namespace Domain.ViewModels.Role;

public class ListRoleViewModel : object
{
    public ListRoleViewModel():base()
    {
    }

    public int Id { get; set; }

    public required string RoleName { get; set; }
}
