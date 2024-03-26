namespace Domain.Entities.Account;

public class Role : IdentityRole<int>, IEntityHasIsDeleted
{
    public Role() : base()
    {
    }

    public bool IsDeleted { get; set; }
}
