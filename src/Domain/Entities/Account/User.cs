namespace Domain.Entities.Account;

public class User : IdentityUser<int>, IEntityHasIsSystemic, IEntityHasIsBanned
{
    public User() : base()
    {
    }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [ForeignKey("Class")]
    public int? ClassId { get; set; }

    public bool IsSystemic { get; set; }

    public bool IsBanned { get; set; }

    #region Relation
    public Class? Class { get; set; }
    #endregion /Relation
}
