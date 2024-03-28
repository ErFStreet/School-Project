namespace Domain.Entities.Learn;

public class Class : BaseEntity<int> , IEntityHasIsDeleted
{
    public required string ClassCode { get; set; }

    public bool IsDeleted { get; set; } 

    #region Relation
    public List<LearnRelation>? LearnRelations { get; set; }

    public List<User>? Users { get; set; }
    #endregion /Relation
}
